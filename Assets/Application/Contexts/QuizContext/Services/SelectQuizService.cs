using System.Collections.Generic;
using System.Linq;
using Application.GameplayContext;
using Application.ProjectContext;
using Application.ProjectContext.Configs;
using Application.ProjectContext.Signals;
using Application.QuizContext.Models;
using Application.Utils;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Application.QuizContext.Services
{
    public class SelectQuizService : MonoBehaviour
    {
        [Inject] private readonly SignalBus _signalBus;
        [Inject] private readonly LearnGameConfig _gameConfig;
        [Inject] private readonly QuizPlayerModel _player;
        [Inject] private readonly SelectedElementService _selectedElementService;
        
        [SerializeField] private SelectionManager _selectionManager;
        [SerializeField] private Button _answerButton;
        [SerializeField] private Text _questionsCounter;
        [SerializeField] private Text _elementToSelectText;

        private List<Transform> _list;
        private string _elementToSelectName;
        private int _elementToSelectIndex = -1;
        private int _totalCount = 1;

        [Inject]
        private void Construct()
        {
            _signalBus.Subscribe<LearnProjectSignals.AnswerGivenSignal>(OnAnswerGivenSignal);
        }

        private void OnAnswerGivenSignal(LearnProjectSignals.AnswerGivenSignal signal)
        { 
            _answerButton.enabled = false;
            if (_selectedElementService.CurrentChosenModelElementView.Name == _elementToSelectName)
            {
                signal.ConfirmButton.GoodAnswer();
                _player.CorrectAnswersCount++;
                DOVirtual.DelayedCall(_gameConfig.PauseTime, NextQuestion);
            }
            else
            {
                signal.ConfirmButton.BadAnswer();
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
            _elementToSelectIndex++;
            if (_elementToSelectIndex == _selectionManager.LearnModelElements.Count)
            {
                _player.SetGameFinished(true);
                _signalBus.Fire(new LearnProjectSignals.GameFinished(new GameResult(_player.CorrectAnswersCount, _player.RemainingTime)));
                return;
            }

            _elementToSelectName = _list[_elementToSelectIndex].gameObject.name;
            _elementToSelectText.text = _elementToSelectName;
            SetClosestElements(_gameConfig.NumberOfClosestElementsToShow);
            _questionsCounter.text = $"{_elementToSelectIndex+1}/{_totalCount}";
            _answerButton.enabled = true;
        }

        private void SetClosestElements(int count)
        {
            var closestObjects = _list
                .OrderBy(obj => Vector3.Distance(obj.transform.position, GameObject.Find(_elementToSelectName).gameObject.transform.position))
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
