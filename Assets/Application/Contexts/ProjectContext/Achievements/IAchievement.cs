namespace Application.ProjectContext.Achievements
{
    public interface IAchievement
    {
        LearnAchievementType Type { get; }
        string TranslationKey { get; }
        bool IsCompleted { get; }
        int Threshold { get; }
        int Progress { get; }
        float ProgressNormalized { get; }
        void SetProgress(int progress);
    }
}