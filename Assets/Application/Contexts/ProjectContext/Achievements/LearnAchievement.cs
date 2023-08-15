using System;
using UnityEngine;
using UnityEngine.UI;

namespace Application.ProjectContext.Achievements
{
    public enum LearnAchievementType
    {
        Answer100Questions = 0,
        Lose1Time = 1,
    }
    [Serializable]
    public class LearnAchievement : IAchievement
    {
        [SerializeField] private LearnAchievementType _type;
        [SerializeField] private string _title;
        private IAchievementProcessor _achievementProcessor;

        public void SetAchievementProcessor(IAchievementProcessor processor)
        {
            _achievementProcessor = processor;
        }

        public LearnAchievementType Type => _type;
        public string Title => _title;
        public IAchievementProcessor Processor => _achievementProcessor;
    }
}