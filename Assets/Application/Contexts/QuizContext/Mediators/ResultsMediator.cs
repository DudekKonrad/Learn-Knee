using System;
using System.Globalization;
using Application.ProjectContext.Signals;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Application.QuizContext.Mediators
{
    public class ResultsMediator : MonoBehaviour
    {
        [Inject] private readonly SignalBus _signalBus;
        [SerializeField] private Text _correctAnswersText;
        [SerializeField] private Text _incorrectAnswersText;
        [SerializeField] private Text _remainingTimeText;
        
        private Sequence _sequence;

        private int _correctAnswersCount, _incorrectAnswersCount;


        private int CorrectAnswersCount
        {
            get => _correctAnswersCount;
            set
            {
                _correctAnswersCount = value;
                _correctAnswersText.text = value.ToString(CultureInfo.InvariantCulture);
            }
        }
        
        private int IncorrectAnswersCount
        {
            get => _incorrectAnswersCount;
            set
            {
                _incorrectAnswersCount = value;
                _incorrectAnswersText.text = value.ToString(CultureInfo.InvariantCulture);
            }
        }
        
        [Inject]
        private void Construct()
        {
            _signalBus.Subscribe<LearnProjectSignals.GameFinished>(OnGameFinishedSignal);
        }

        private void Start()
        {
            throw new NotImplementedException();
        }

        private void OnGameFinishedSignal(LearnProjectSignals.GameFinished signal)
        {
            transform.DOLocalMoveY(0, 0.2f).OnComplete(() =>
            {
                _sequence.Append(DOTween.To(() => CorrectAnswersCount, _ => CorrectAnswersCount = _,signal.CorrectAnswers,
                    0.4f).SetLink(gameObject));
                _sequence.Append(DOTween.To(() => IncorrectAnswersCount, _ => IncorrectAnswersCount = _,signal.IncorrectAnswers,
                    0.4f).SetLink(gameObject));
            });
        }
        
        private void CalculateScore()
        {
        
        }
    }
}
