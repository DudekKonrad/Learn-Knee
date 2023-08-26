namespace Application.QuizContext.Services
{
    public enum QuizType
    {
        Easy = 0,
        Medium = 1,
        Hard = 2
    }
    
    public class GameResult
    {
        private int _correctAnswersCount;
        private int _incorrectAnswersCount;
        private float _remainingTime;
        private QuizType _quizType;

        public GameResult(int correctAnswersCount, int incorrectAnswers, float remainingTime, QuizType type)
        {
            _correctAnswersCount = correctAnswersCount;
            _incorrectAnswersCount = incorrectAnswers;
            _remainingTime = remainingTime;
        }


        public int CorrectAnswersCount => _correctAnswersCount;
        public int IncorrectAnswersCount => _incorrectAnswersCount;
        public float RemainingTime => _remainingTime;
        public QuizType QuizType => _quizType;
    }
}