using System;
using System.Globalization;
using Application.GameplayContext.Models;
using Application.ProjectContext.Signals;
using Application.QuizContext.Models;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Application.QuizContext.Mediators
{
    public class ResultsMediator : MonoBehaviour
    {
        [Inject] private readonly SignalBus _signalBus;
        [Inject] private readonly QuizPlayerModel _player;
        
        [SerializeField] private Text _correctAnswersText;
        [SerializeField] private Text _incorrectAnswersText;
        [SerializeField] private Text _remainingTimeText;
        
        private Sequence _sequence;

        private int _correctAnswersCount, _incorrectAnswersCount;
        private float _remainingTime;


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
        private float RemainingTime
        {
            get => _remainingTime;
            set
            {
                _remainingTime = value;
                var timeSpan = TimeSpan.FromSeconds(_remainingTime);
                var timeFormatted = timeSpan.ToString(@"mm\:ss");
                _remainingTimeText.text = timeFormatted;
            }
        }
        
        [Inject]
        private void Construct()
        {
            _signalBus.Subscribe<LearnProjectSignals.GameFinished>(OnGameFinishedSignal);
        }

        private void OnGameFinishedSignal(LearnProjectSignals.GameFinished signal)
        {
            Debug.Log($"Results game finished");
            transform.DOLocalMoveY(0, 0.2f).OnComplete(() =>
            {
                Debug.Log($"Complete do local move y");
                _sequence.Append(DOTween.To(() => CorrectAnswersCount, _ => CorrectAnswersCount = _,_player.CorrectAnswersCount,
                    0.4f).SetLink(gameObject));
                _sequence.Append(DOTween.To(() => IncorrectAnswersCount, _ => IncorrectAnswersCount = _,_player.IncorrectAnswersCount,
                    0.4f).SetLink(gameObject));
                _sequence.Append(DOTween.To(() => RemainingTime, _ => RemainingTime = _,_player.RemainingTime,
                    0.4f).SetLink(gameObject));
            });
        }
        
        private void CalculateScore()
        {
        
        }
    }
}
