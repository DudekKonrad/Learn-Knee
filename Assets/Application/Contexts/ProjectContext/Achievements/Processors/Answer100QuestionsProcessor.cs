using System;

namespace Application.ProjectContext.Achievements.Processors
{
    public class Answer100QuestionsProcessor : AnswerXQuestionsProcessor
    {
        public override int Threshold => 100;
        public override Enum AchievementType => LearnAchievementType.Answer100Questions;
    }
}