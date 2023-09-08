using System.Collections.Generic;
using Application.GameplayContext;
using Application.ProjectContext;
using Application.ProjectContext.Configs;
using Application.ProjectContext.Signals;
using Application.QuizContext.Mediators;
using Application.QuizContext.Models;
using Application.Utils;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Application.QuizContext.Services
{
    public class InputQuizService : MonoBehaviour
    {
        [Inject] private readonly SignalBus _signalBus;
        [Inject] private readonly LearnGameConfig _gameConfig;
        [Inject] private readonly QuizPlayerModel _player;
        [Inject] private readonly SelectedElementService _selectedElementService;

        
        [SerializeField] private SelectionManager _selectionManager;
        [SerializeField] private InputAnswerMediator _answerInputMediator;
        [SerializeField] private Text _questionsCounter;

        private List<Transform> _list;
        private ModelElementView _currentElementView;
        private int _currentElementIndex = -1;
        private int _totalCount = 1;
        
        [Inject]
        private void Construct()
        {
            _signalBus.Subscribe<LearnProjectSignals.AnswerGivenSignal>(OnAnswerGivenSignal);
        }

        private void Start()
        {
            _list = new List<Transform>(_selectionManager.LearnModelElements);
            _totalCount = _selectionManager.LearnModelElements.Count;
            ListExtensionMethods.Shuffle(_list);
            NextQuestion();
        }

        private void OnAnswerGivenSignal(LearnProjectSignals.AnswerGivenSignal signal)
        { 
            _answerInputMediator.enabled = false;
            if (StringExtensionMethods.CompareNormalizedStrings(_currentElementView.GameObject.name, signal.AnswerString))
            {
                _player.CorrectAnswersCount++;
                _answerInputMediator.GoodAnswer();
                DOVirtual.DelayedCall(_gameConfig.PauseTime, NextQuestion);
            }
            else
            {
                _player.IncorrectAnswersCount++;
                _answerInputMediator.BadAnswer();
                DOVirtual.DelayedCall(_gameConfig.PauseTime, NextQuestion);
            }
        }

        private void FinishGame(QuizResult result)
        {
            _player.SetGameFinished(true);
            _signalBus.Fire(new LearnProjectSignals.GameFinished(
                new GameResult(_player.CorrectAnswersCount, _player.IncorrectAnswersCount,
                    _player.RemainingTime, QuizType.Easy, result, _player.TotalScore)));
        }
        
        private void NextQuestion()
        {
            _answerInputMediator.SetDefault();
            _currentElementIndex++;
            if (_currentElementIndex == _selectionManager.LearnModelElements.Count)
            {
                FinishGame(_player.CorrectAnswersCount > _selectionManager.LearnModelElements.Count / 2
                    ? QuizResult.Win
                    : QuizResult.Lose);
                return;
            }

            _currentElementView = _list[_currentElementIndex].GetComponent<ModelElementView>();
            _currentElementView.OnChosen();
            SetNeighbours(_selectedElementService.CurrentChosenModelElementView);
            _selectedElementService.CurrentChosenModelElementView.Expose();
            _questionsCounter.text = $"{_currentElementIndex+1}/{_totalCount}";
            _answerInputMediator.enabled = true;
        }
        
        private void SetNeighbours(ModelElementView element)
        {
            if (element.AllNeighbour)
            {
                foreach (var e in _selectionManager.LearnModelElements)
                {
                    e.gameObject.SetActive(true);
                }
            }
            else
            {
                foreach (var e in _selectionManager.LearnModelElements)
                {
                    e.gameObject.SetActive(false);
                    if (e.name == _selectedElementService.CurrentChosenModelElementView.Name)
                    {
                        e.gameObject.SetActive(true);
                    }
                }
                foreach (var neighbour in element.Neighbours)
                {
                    neighbour.gameObject.SetActive(true);
                }
            }
        }

    }
}
