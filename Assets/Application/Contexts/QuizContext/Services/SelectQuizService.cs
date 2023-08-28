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
    public class SelectQuizService : MonoBehaviour
    {
        [Inject] private readonly SignalBus _signalBus;
        [Inject] private readonly LearnGameConfig _gameConfig;
        [Inject] private readonly QuizPlayerModel _player;
        [Inject] private readonly SelectedElementService _selectedElementService;
        
        [SerializeField] private SelectionManager _selectionManager;
        [SerializeField] private Button _answerButton;
        [SerializeField] private Text _questionsCounter;
        [SerializeField] private LocalizedText _elementToSelectText;
        [SerializeField] private ConfirmButtonMediator _confirmButtonMediator;

        private List<Transform> _list;
        private ModelElementView _elementToSelectView;
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
            if (_selectedElementService.CurrentChosenModelElementView.ElementType == _elementToSelectView.ElementType)
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

        private void FinishGame(QuizResult result)
        {
            _player.SetGameFinished(true);
            _signalBus.Fire(new LearnProjectSignals.GameFinished(
                new GameResult(_player.CorrectAnswersCount, _player.IncorrectAnswersCount,
                    _player.RemainingTime, QuizType.Easy, result)));
        }
        
        private void NextQuestion()
        {
            _confirmButtonMediator.SetTextDefaultColor();
            _elementToSelectIndex++;
            if (_elementToSelectIndex == _selectionManager.LearnModelElements.Count)
            {
                FinishGame(_player.CorrectAnswersCount > _selectionManager.LearnModelElements.Count / 2
                    ? QuizResult.Win
                    : QuizResult.Lose);
                return;
            }

            _elementToSelectView = _list[_elementToSelectIndex].GetComponent<ModelElementView>();
            _elementToSelectText.SetTranslationKey(_elementToSelectView.TranslationKey);
            _selectedElementService.CurrentChosenModelElementView.Expose();
            _questionsCounter.text = $"{_elementToSelectIndex+1}/{_totalCount}";
            _answerButton.enabled = true;
        }
    }
}
