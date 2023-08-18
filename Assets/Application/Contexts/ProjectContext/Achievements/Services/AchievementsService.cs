using System.Collections.Generic;
using Application.ProjectContext.Signals;
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
            
        }

        private void OnAnswerGivenSignal(LearnProjectSignals.AnswerGivenSignal signal)
        {
            _answersCounter++;
            _achievements[LearnAchievementType.Answer100Questions].SetProgress(_answersCounter);
        }

        public IAchievementService.AchievementProgress GetProgress(IAchievement achievement)
        {
            var achievementProgress = _achievements[achievement.Type];
            return new IAchievementService.AchievementProgress(achievementProgress.IsCompleted, achievementProgress.Progress,
                achievementProgress.ProgressNormalized, achievementProgress.Threshold);
        }
    }
}