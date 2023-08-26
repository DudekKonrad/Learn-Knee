using Application.ProjectContext.Signals;
using Zenject;

namespace Application.ProjectContext.Services
{
    public class LeaderboardService
    {
        [Inject] private readonly SignalBus _signalBus;
        public int HighScore;

        [Inject]
        private void Construct()
        {
            _signalBus.Subscribe<LearnProjectSignals.GameFinished>(OnGameFinished);
        }

        private void OnGameFinished(LearnProjectSignals.GameFinished signal)
        {
            HighScore = signal.GameResult.CorrectAnswersCount;
        }
    }
}