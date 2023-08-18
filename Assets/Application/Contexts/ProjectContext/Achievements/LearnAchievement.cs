using System;
using UnityEngine;

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
        [SerializeField] private string _translationKey;
        [SerializeField] private int _threshold;
        [SerializeField] private int _progress;
        
        public LearnAchievementType Type => _type;
        public string TranslationKey => _translationKey;
        public bool IsCompleted => Progress >= Threshold;
        public int Threshold => _threshold;
        public int Progress => _progress;
        public float ProgressNormalized => Mathf.Clamp01((float)Progress/Threshold);

        public void SetProgress(int progress)
        {
            _progress = progress;
        }
    }
}