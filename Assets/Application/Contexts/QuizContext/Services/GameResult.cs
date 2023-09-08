namespace Application.QuizContext.Services
{
    public enum QuizType
    {
        Easy = 0,
        Medium = 1,
        Hard = 2
    }

    public enum QuizResult
    {
        Lose = 0,
        Win = 1,
        TimeIsUp = 2
    }
    
    public class GameResult
    {
        private int _correctAnswersCount;
        private int _incorrectAnswersCount;
        private int _totalScore;
        private float _remainingTime;
        private QuizType _quizType;
        private QuizResult _quizResult;

        public GameResult(int correctAnswersCount, int incorrectAnswers, float remainingTime, QuizType type, QuizResult result, int totalScore)
        {
            _correctAnswersCount = correctAnswersCount;
            _incorrectAnswersCount = incorrectAnswers;
            _remainingTime = remainingTime;
            _quizType = type;
            _quizResult = result;
            _totalScore = totalScore;
        }


        public int CorrectAnswersCount => _correctAnswersCount;
        public int IncorrectAnswersCount => _incorrectAnswersCount;
        public int TotalScore => _totalScore;
        public float RemainingTime => _remainingTime;
        public QuizType QuizType => _quizType;
        public QuizResult QuizResult => _quizResult;
    }
}