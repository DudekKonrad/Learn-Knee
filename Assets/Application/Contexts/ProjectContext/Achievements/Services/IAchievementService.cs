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

            public AchievementProgress(bool isCompleted, int progress, float progressNormalized, int threshold)
            {
                IsCompleted = isCompleted;
                Progress = progress;
                ProgressNormalized = progressNormalized;
                Threshold = threshold;
            }
        }
        
        AchievementProgress GetProgress(IAchievement achievement);
    }
}