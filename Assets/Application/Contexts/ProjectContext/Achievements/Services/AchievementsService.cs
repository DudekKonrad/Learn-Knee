using System;
using System.Collections.Generic;
using Application.ProjectContext.Achievements.Processors;
using JetBrains.Annotations;
using Zenject;

namespace Application.ProjectContext.Achievements.Services
{
    [UsedImplicitly]
    public class AchievementsService : IAchievementService
    {
        [Inject] private readonly IAchievementsConfig _achievementsConfig;
        
        private readonly Dictionary<LearnAchievementType, IAchievementProcessor> _achievementProcessors =
            new Dictionary<LearnAchievementType, IAchievementProcessor>();

        [Inject]
        private void Construct()
        {
            foreach (var achievement in _achievementsConfig.Achievements)
            {
                var processor = GetProcessorForAchievement(achievement);
                _achievementProcessors.Add(achievement.Type, processor);
            }
        }

        private IAchievementProcessor GetProcessorForAchievement(IAchievement achievement)
        {
            switch (achievement.Type)
            {
                case LearnAchievementType.Answer100Questions:
                    return new Answer100QuestionsProcessor();
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        public (bool, string, float, bool) GetProgress(IAchievement achievement)
        {
            var achievementProcessor = _achievementProcessors[achievement.Type];
            return (achievementProcessor.IsCompleted, achievementProcessor.ProgressLabel,
                achievementProcessor.ProgressNormalized, achievementProcessor.ShowProgress);
        }
    }
}