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
using Random = UnityEngine.Random;

namespace Application.QuizContext.Services
{
    public class QuizService : MonoBehaviour
    {
        [Inject] private readonly SignalBus _signalBus;
        [Inject] private readonly LearnGameConfig _gameConfig;
        [Inject] private readonly QuizPlayerModel _player;
        [Inject] private readonly SelectedElementService _selectedElementService;

        
        [SerializeField] private SelectionManager _selectionManager;
        [SerializeField] private List<AnswerButtonMediator> _answerButtons;
        [SerializeField] private Text _questionsCounter;

        private List<Transform> _list;
        private ISelectionResponse _currentElement;
        private ModelElementView _currentElementView;
        private int _currentElementIndex = -1;
        private int _totalCount = 1;

        [Inject]
        private void Construct()
        {
            _signalBus.Subscribe<LearnProjectSignals.AnswerGivenSignal>(OnAnswerGivenSignal);
        }

        private void OnAnswerGivenSignal(LearnProjectSignals.AnswerGivenSignal signal)
        {
            foreach (var button in _answerButtons)
            {
                button.Button.enabled = false;
            }
            if (_currentElementView.ElementType == signal.AnswerType)
            {
                signal.AnswerButton.GoodAnswer();
                _player.CorrectAnswersCount++;
                DOVirtual.DelayedCall(_gameConfig.PauseTime, NextQuestion);
            }
            else
            {
                signal.AnswerButton.BadAnswer();
                _player.IncorrectAnswersCount++;
                DOVirtual.DelayedCall(_gameConfig.PauseTime, NextQuestion);
            }
        }

        private void Start()
        {
            _list = new List<Transform>(_selectionManager.LearnModelElements);
            _totalCount = _selectionManager.LearnModelElements.Count;
            ListExtensionMethods.Shuffle(_list);
            NextQuestion();
        }

        private void FinishGame(QuizResult result)
        {
            _player.SetGameFinished(true);
            _signalBus.Fire(new LearnProjectSignals.GameFinished(
                new GameResult(_player.CorrectAnswersCount, _player.IncorrectAnswersCount,
                    _player.RemainingTime, QuizType.Easy, result)));
        }
        private void NextQuestion()
        {
            _currentElementIndex++;
            if (_currentElementIndex == _selectionManager.LearnModelElements.Count)
            {
                FinishGame(_player.CorrectAnswersCount > _selectionManager.LearnModelElements.Count / 2
                    ? QuizResult.Win
                    : QuizResult.Lose);
                return;
            }

            _currentElement = _list[_currentElementIndex].GetComponent<ISelectionResponse>();
            _currentElementView = _currentElement.GameObject.GetComponent<ModelElementView>();
            _currentElement.OnChosen();
            SetNeighbours(_selectedElementService.CurrentChosenModelElementView);
            _selectedElementService.CurrentChosenModelElementView.Expose();
            _questionsCounter.text = $"{_currentElementIndex+1}/{_totalCount}";
            foreach (var button in _answerButtons)
            {
                button.Button.enabled = true;
            }

            var randomElements = new List<Transform>(_list);
            var randomButtonIndex = Random.Range(0, _answerButtons.Count);
            for (var i = 0; i < _answerButtons.Count; i++)
            {
                if (i == randomButtonIndex)
                {
                    randomElements.Remove(_currentElement.GameObject.transform);
                    _answerButtons[i].LocalizedText.SetTranslationKey(_currentElementView.TranslationKey);
                    _answerButtons[i].ButtonElementType = _currentElementView.ElementType;
                    _answerButtons[i].SetTextDefaultColor();
                }
                else
                {
                    randomElements.Remove(_currentElement.GameObject.transform);
                    var element = randomElements.GetRandomElement();
                    randomElements.Remove(element);
                    var elementView = element.GetComponent<ModelElementView>();
                    _answerButtons[i].LocalizedText.SetTranslationKey(elementView.TranslationKey);
                    _answerButtons[i].ButtonElementType = elementView.ElementType;
                    _answerButtons[i].SetTextDefaultColor();
                }
            }
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
