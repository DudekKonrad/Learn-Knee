using System;

namespace Application.ProjectContext.Achievements
{
    public interface IAchievementProcessor
    {
        Enum AchievementType { get; }
        bool IsCompleted { get; }
        int Threshold { get; }
        int Progress { get; }
        float ProgressNormalized { get; }
        bool ShowProgress { get; }
        string ProgressLabel { get; }
    }
}