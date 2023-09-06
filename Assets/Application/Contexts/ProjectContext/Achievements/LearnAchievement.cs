using System;
using UnityEngine;

namespace Application.ProjectContext.Achievements
{
    public enum LearnAchievementType
    {
        Answer200Questions,
        Answer100Questions,
        Answer50Questions,
        SolveEasyQuizWithoutMistake,
        SolveEasyQuizWith75PercentCorrectAnswers, 
        SolveEasyQuizWith50PercentCorrectAnswers,
        SolveMediumQuizWithoutMistake,
        SolveMediumQuizWith75PercentCorrectAnswers, 
        SolveMediumQuizWith50PercentCorrectAnswers,
        SolveHardQuizWithoutMistake,
        SolveHardQuizWith75PercentCorrectAnswers, 
        SolveHardQuizWith50PercentCorrectAnswers,
        SolveAnyQuizUnder2Minutes,
        SolveAnyQuizUnder1Minute,
        SolveAnyQuizUnder30Seconds,
        SolveQuizInLast5Seconds,
        ReachFirstPlaceInLeaderBoard,
        ReachPlaceOnPodiumInLeaderBoard,
        SolveQuizOnEachDifficultyLevel,
    }
    
    [Serializable]
    public class LearnAchievement : IAchievement
    {
        [SerializeField] private LearnAchievementType _type;
        [SerializeField] private string _translationKey;
        [SerializeField] private int _threshold;
        [SerializeField] private bool _isProgressVisible;
        
        public LearnAchievementType Type => _type;
        public string TranslationKey => _translationKey;
        public bool IsCompleted => Progress >= Threshold;
        public int Threshold => _threshold;
        public int Progress => PlayerPrefs.GetInt(_type.ToString());
        public bool IsProgressVisible => _isProgressVisible;
        public float ProgressNormalized => Mathf.Clamp01((float)Progress/Threshold);

        public void SetProgress(int progress)
        {
            PlayerPrefs.SetInt(_type.ToString(), progress);
        }
    }
}