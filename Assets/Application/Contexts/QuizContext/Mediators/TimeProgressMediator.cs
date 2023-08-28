using System;
using Application.GameplayContext.Models;
using Application.ProjectContext.Configs;
using Application.ProjectContext.Signals;
using Application.QuizContext.Models;
using Application.QuizContext.Services;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Application.QuizContext.Mediators
{
    public class TimeProgressMediator : MonoBehaviour
    {
        [Inject] private readonly SignalBus _signalBus;
        [Inject] private readonly LearnGameConfig _gameConfig;
        [Inject] private readonly QuizPlayerModel _player;

        [SerializeField] private Image _image;
        [SerializeField] private Text _timeLeftText;
        public float _startingTime, _actualTime;
        private float _remainingTimePercent => _actualTime / _startingTime;
        private float _elapsedTimeInSeconds;
    
        private void Start()
        {
            _startingTime = _gameConfig.QuizModeConfig.Duration;
            _actualTime = _startingTime;
        }
    
        private void Update()
        {
            if (_actualTime > 0 && !_player.IsGameFinished)
            {
                _actualTime -= Time.deltaTime;
                _elapsedTimeInSeconds += Time.deltaTime;
                _player.SetRemainingTime(_actualTime);
                _image.fillAmount = _remainingTimePercent;
                var timeSpan = TimeSpan.FromSeconds(_actualTime);
                var timeFormatted = timeSpan.ToString(@"mm\:ss");
                _timeLeftText.text = timeFormatted;
            }
            else
            {
                _signalBus.Fire(new LearnProjectSignals.GameFinished(
                    new GameResult(_player.CorrectAnswersCount, _player.IncorrectAnswersCount,
                        _player.RemainingTime, QuizType.Easy, QuizResult.TimeIsUp)));
            }
        }
    }
}