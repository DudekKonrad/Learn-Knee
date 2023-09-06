using System;
using System.Collections.Generic;
using Application.ProjectContext.Signals;
using Application.QuizContext.Services;
using JetBrains.Annotations;
using Zenject;

namespace Application.ProjectContext.Achievements.Services
{
    [UsedImplicitly]
    public class AchievementsService : IAchievementService
    {
        [Inject] private readonly IAchievementsConfig _achievementsConfig;
        [Inject] private readonly SignalBus _signalBus;
        
        private readonly Dictionary<LearnAchievementType, IAchievement> _achievements =
            new Dictionary<LearnAchievementType, IAchievement>();

        private int _answersCounter;
        private int _correctAnswersStreakCounter;

        [Inject]
        private void Construct()
        {
            foreach (var achievement in _achievementsConfig.Achievements)
            {
                _achievements.Add(achievement.Type, achievement);
            }
            
            _signalBus.Subscribe<LearnProjectSignals.AnswerGivenSignal>(OnAnswerGivenSignal);
            _signalBus.Subscribe<LearnProjectSignals.GameFinished>(OnGameFinishedSignal);
        }

        private void OnGameFinishedSignal(LearnProjectSignals.GameFinished signal)
        {
            switch (signal.GameResult.QuizType)
            {
                case QuizType.Easy:
                    UpdateAnswersAchievementProgress(LearnAchievementType.SolveEasyQuizWith50PercentCorrectAnswers, signal.GameResult, 0.5f);
                    UpdateAnswersAchievementProgress(LearnAchievementType.SolveEasyQuizWith75PercentCorrectAnswers, signal.GameResult, 0.75f);
                    UpdateAnswersAchievementProgress(LearnAchievementType.SolveEasyQuizWithoutMistake, signal.GameResult, 1f); 
                    CheckTimeAchievements(signal.GameResult);
                    break;
                case QuizType.Medium:
                    UpdateAnswersAchievementProgress(LearnAchievementType.SolveMediumQuizWith50PercentCorrectAnswers, signal.GameResult, 0.5f);
                    UpdateAnswersAchievementProgress(LearnAchievementType.SolveMediumQuizWith75PercentCorrectAnswers, signal.GameResult, 0.75f);
                    UpdateAnswersAchievementProgress(LearnAchievementType.SolveMediumQuizWithoutMistake, signal.GameResult, 1f);
                    CheckTimeAchievements(signal.GameResult);

                    break;
                case QuizType.Hard:
                    UpdateAnswersAchievementProgress(LearnAchievementType.SolveHardQuizWith50PercentCorrectAnswers, signal.GameResult, 0.5f);
                    UpdateAnswersAchievementProgress(LearnAchievementType.SolveHardQuizWith75PercentCorrectAnswers, signal.GameResult, 0.75f);
                    UpdateAnswersAchievementProgress(LearnAchievementType.SolveHardQuizWithoutMistake, signal.GameResult, 1f);
                    CheckTimeAchievements(signal.GameResult);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        private void UpdateAnswersAchievementProgress(LearnAchievementType achievementType, GameResult gameResult, float threshold)
        {
            var progress = (float)gameResult.CorrectAnswersCount / gameResult.IncorrectAnswersCount;
            _achievements[achievementType].SetProgress(progress >= threshold ? 1 : 0);
        }
        private void UpdateTimeAchievementProgress(LearnAchievementType achievementType, GameResult gameResult, float threshold)
        {
            var progress = gameResult.RemainingTime;
            _achievements[achievementType].SetProgress(progress >= threshold ? 1 : 0);
        }

        private void CheckTimeAchievements(GameResult gameResult)
        {
            UpdateTimeAchievementProgress(LearnAchievementType.SolveAnyQuizUnder2Minutes, gameResult,_achievements[LearnAchievementType.SolveAnyQuizUnder2Minutes].Threshold);
            UpdateTimeAchievementProgress(LearnAchievementType.SolveAnyQuizUnder1Minute, gameResult, _achievements[LearnAchievementType.SolveAnyQuizUnder1Minute].Threshold);
            UpdateTimeAchievementProgress(LearnAchievementType.SolveAnyQuizUnder30Seconds, gameResult, _achievements[LearnAchievementType.SolveAnyQuizUnder30Seconds].Threshold);
            UpdateTimeAchievementProgress(LearnAchievementType.SolveQuizInLast5Seconds, gameResult, _achievements[LearnAchievementType.SolveQuizInLast5Seconds].Threshold);
        }

        private void OnAnswerGivenSignal(LearnProjectSignals.AnswerGivenSignal signal)
        {
            _answersCounter++;
            _achievements[LearnAchievementType.Answer50Questions].SetProgress(_answersCounter);
            _achievements[LearnAchievementType.Answer100Questions].SetProgress(_answersCounter);
            _achievements[LearnAchievementType.Answer200Questions].SetProgress(_answersCounter);
        }
    }
}