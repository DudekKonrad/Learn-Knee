using Application.ProjectContext.Signals;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class ResultsMediator : MonoBehaviour
{
    [Inject] private readonly SignalBus _signalBus;
    [SerializeField] private Text _correctAnswersText;
    [SerializeField] private Text _incorrectAnswersText;
    [SerializeField] private Text _remainingTimeText;

    [Inject]
    private void Construct()
    {
        _signalBus.Subscribe<LearnProjectSignals.GameFinished>(OnGameFinishedSignal);
    }

    private void OnGameFinishedSignal(LearnProjectSignals.GameFinished signal)
    {
        transform.DOLocalMoveY(0, 0.2f).OnComplete(() =>
        {
            Debug.Log($"Correct: {signal.CorrectAnswers}");
            Debug.Log($"INCorrect: {signal.IncorrectAnswers}");
            _correctAnswersText.text = $"Poprawne odpowiedzi: {signal.CorrectAnswers}";
            _incorrectAnswersText.text = $"Niepoprawne odpowiedzi: {signal.IncorrectAnswers}";
        });
    }

    private void CalculateScore()
    {
        
    }
}
