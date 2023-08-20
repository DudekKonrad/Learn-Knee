namespace Application.QuizContext.Services
{
    public class GameResult
    {
        public int Score;
        public float RemainingTime;
        public GameResult(int score, float remainingTime)
        {
            Score = score;
            RemainingTime = remainingTime;
        }
    }
}