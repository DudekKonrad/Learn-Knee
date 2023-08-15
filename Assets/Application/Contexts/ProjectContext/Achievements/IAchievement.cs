using System;

namespace Application.ProjectContext.Achievements
{
    public interface IAchievement
    {
        LearnAchievementType Type { get; }
        string Title { get; }
        IAchievementProcessor Processor { get; }
    }
}