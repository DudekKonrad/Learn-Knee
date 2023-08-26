using System.Collections.Generic;
using System.Linq;
using Application.GameplayContext;
using Application.ProjectContext.Configs;
using Application.ProjectContext.Services;
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
    public class InputQuizService : MonoBehaviour
    {
        [Inject] private readonly SignalBus _signalBus;
        [Inject] private readonly LearnGameConfig _gameConfig;
        [Inject] private readonly QuizPlayerModel _player;
        
        [SerializeField] private SelectionManager _selectionManager;
        [SerializeField] private InputField _answerInputField;
        [SerializeField] private Text _questionsCounter;

        private List<Transform> _list;
        private ISelectionResponse _currentElement;
        private int _currentElementIndex = -1;
        private int _totalCount = 1;

        [Inject]
        private void Construct()
        {
            _signalBus.Subscribe<LearnProjectSignals.AnswerGivenSignal>(OnAnswerGivenSignal);
        }

        private void OnAnswerGivenSignal(LearnProjectSignals.AnswerGivenSignal signal)
        { 
            _answerInputField.enabled = false;
            if (_currentElement.GameObject.name == signal.Answer)
            {
                _player.CorrectAnswersCount++;
                DOVirtual.DelayedCall(_gameConfig.PauseTime, NextQuestion);
            }
            else
            {
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

        private void NextQuestion()
        {
            _currentElementIndex++;
            if (_currentElementIndex == _selectionManager.LearnModelElements.Count)
            {
                _player.SetGameFinished(true);
                _signalBus.Fire(new LearnProjectSignals.GameFinished(
                    new GameResult(_player.CorrectAnswersCount, _player.IncorrectAnswersCount,
                        _player.RemainingTime, QuizType.Hard)));
                return;
            }

            _currentElement = _list[_currentElementIndex].GetComponent<ISelectionResponse>();
            SetClosestElements(_gameConfig.NumberOfClosestElementsToShow);
            _currentElement.OnChosen();
            _questionsCounter.text = $"{_currentElementIndex+1}/{_totalCount}";
            _answerInputField.enabled = true;
        }

        private void SetClosestElements(int count)
        {
            var closestObjects = _list
                .OrderBy(obj => Vector3.Distance(obj.transform.position, _currentElement.GameObject.transform.position))
                .Take(count)
                .ToArray();
        
            foreach (var element in closestObjects)
            {
                element.gameObject.SetActive(true);
            }
        
            var remainingObjects = _list.Except(closestObjects).ToArray();
            foreach (var element in remainingObjects)
            {
                element.gameObject.SetActive(false);
            }
        }
    }
}
