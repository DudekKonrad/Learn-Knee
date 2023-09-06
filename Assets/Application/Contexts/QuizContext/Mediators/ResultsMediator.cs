using System;
using System.Globalization;
using Application.ProjectContext.Configs;
using Application.ProjectContext.Signals;
using Application.QuizContext.Models;
using Application.QuizContext.Services;
using Application.Utils.SoundService;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Application.QuizContext.Mediators
{
    public class ResultsMediator : MonoBehaviour
    {
        [Inject] private readonly SignalBus _signalBus;
        [Inject] private readonly LearnGameConfig _gameConfig;
        [Inject] private readonly QuizPlayerModel _player;
        
        [SerializeField] private Text _correctAnswersText;
        [SerializeField] private Text _incorrectAnswersText;
        [SerializeField] private Text _remainingTimeText;
        [SerializeField] private Text _totalScoreText;
        [SerializeField] private GameObject _confetti;
        [SerializeField] private GameObject _timeIsUpPanel;
        [SerializeField] private Image _starIcon;
        
        private Sequence _sequence;

        private int _correctAnswersCount, _incorrectAnswersCount, _totalScore;
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
        
        private int TotalScore
        {
            get => _totalScore;
            set
            {
                _totalScore = value;
                _totalScoreText.text = _totalScore.ToString(CultureInfo.InvariantCulture);
            }
        }
        
        [Inject]
        private void Construct()
        {
            _signalBus.Subscribe<LearnProjectSignals.GameFinished>(OnGameFinishedSignal);
        }

        private void OnGameFinishedSignal(LearnProjectSignals.GameFinished signal)
        {
            switch (signal.GameResult.QuizResult)
            {
                case QuizResult.Lose:
                    _signalBus.Fire(new LearnProjectSignals.PlaySoundSignal(AudioClipModel.UISounds.OnLose));
                    break;
                case QuizResult.Win:
                    _signalBus.Fire(new LearnProjectSignals.PlaySoundSignal(AudioClipModel.UISounds.OnWin));
                    break;
                case QuizResult.TimeIsUp:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            if (signal.GameResult.QuizResult == QuizResult.TimeIsUp)
            {
                _timeIsUpPanel.SetActive(true);
                _confetti.SetActive(false);
                transform.DOLocalMoveY(0, 0.2f);
                return;
            }
            transform.DOLocalMoveY(0, 0.2f).OnComplete(() =>
            {
                _confetti.SetActive(true);
                _sequence = DOTween.Sequence();
                _sequence.Append(DOTween.To(() => CorrectAnswersCount, _ => CorrectAnswersCount = _,_player.CorrectAnswersCount,
                    _gameConfig.CalculatingResultDuration));
                _sequence.Append(DOTween.To(() => IncorrectAnswersCount, _ => IncorrectAnswersCount = _,_player.IncorrectAnswersCount,
                    _gameConfig.CalculatingResultDuration));
                _sequence.Append(DOTween.To(() => RemainingTime, _ => RemainingTime = _,_player.RemainingTime,
                    _gameConfig.CalculatingResultDuration));
                _sequence.Append(DOTween.To(() => TotalScore, _ => TotalScore = _,_player.TotalScore,
                    _gameConfig.CalculatingResultDuration));
                _sequence.Append(_starIcon.DOFillAmount(1f, _gameConfig.CalculatingResultDuration));
            });
        }
    }
}
