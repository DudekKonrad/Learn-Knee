namespace Application.ProjectContext.Achievements.Services
{
    public interface IAchievementService
    {
        (bool, string, float, bool) GetProgress(IAchievement achievement);
    }
}