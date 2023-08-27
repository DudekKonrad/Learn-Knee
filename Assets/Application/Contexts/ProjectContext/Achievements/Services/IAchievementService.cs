namespace Application.ProjectContext.Achievements.Services
{
    public interface IAchievementService
    {
        class AchievementProgress
        {
            public bool IsCompleted;
            public float ProgressNormalized;
            public int Progress;
            public int Threshold;
            public bool IsProgressVisible;

            public AchievementProgress(bool isCompleted, int progress, float progressNormalized, int threshold, bool isProgressVisible)
            {
                IsCompleted = isCompleted;
                Progress = progress;
                ProgressNormalized = progressNormalized;
                Threshold = threshold;
                IsProgressVisible = isProgressVisible;
            }
        }
        
        AchievementProgress GetProgress(IAchievement achievement);
    }
}