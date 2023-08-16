using JetBrains.Annotations;

namespace Application.QuizContext.Models
{
    [UsedImplicitly]
    public class QuizPlayerModel
    {
        private float _remainingTime;
        private bool _isGameFinished;
        private int _correctAnswers, _incorrectAnswers;

        public int CorrectAnswersCount
        {
            get => _correctAnswers;
            set
            {
                _correctAnswers = value;
            }
        }
        public int IncorrectAnswersCount
        {
            get => _incorrectAnswers;
            set
            {
                _incorrectAnswers = value;
            }
        }

        public float RemainingTime => _remainingTime;
        public bool IsGameFinished => _isGameFinished;

        public void SetRemainingTime(float time)
        {
            _remainingTime = time;
        }

        public void SetGameFinished(bool value)
        {
            _isGameFinished = value;
        }
    }
}