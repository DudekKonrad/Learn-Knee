using System;
using System.Collections.Generic;
using Application.ProjectContext.Signals;
using Application.QuizContext.Services;
using JetBrains.Annotations;
using UnityEngine;
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
            var progress = (float)gameResult.CorrectAnswersCount / (gameResult.CorrectAnswersCount+gameResult.IncorrectAnswersCount);
            Debug.Log($"Update achievement percent progress");
            Debug.Log($"Progress: {progress}");
            Debug.Log($"Threshold: {threshold}");
            var progressInPercent = progress * 100;
            _achievements[achievementType].SetProgress((int)progressInPercent);
        }
        private void UpdateTimeAchievementProgress(LearnAchievementType achievementType, GameResult gameResult, float threshold)
        {
            var progress = gameResult.RemainingTime;
            _achievements[achievementType].SetProgress((int)progress);
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
            var answersCount = PlayerPrefs.GetInt("AnswersCounter");
            answersCount++;
            PlayerPrefs.SetInt("AnswersCounter", answersCount);
            _achievements[LearnAchievementType.Answer50Questions].SetProgress(answersCount);
            _achievements[LearnAchievementType.Answer100Questions].SetProgress(answersCount);
            _achievements[LearnAchievementType.Answer200Questions].SetProgress(answersCount);
        }
    }
}