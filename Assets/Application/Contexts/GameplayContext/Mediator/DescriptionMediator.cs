using System;
using Application.ProjectContext.Configs;
using Application.ProjectContext.Models;
using Application.ProjectContext.Signals;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Application.GameplayContext.Mediator
{
    [RequireComponent(typeof(Text))]
    public class DescriptionMediator : MonoBehaviour
    {
        [Inject] private readonly SignalBus _signalBus;
        [Inject] private readonly DescriptionsModel _descriptions;
        [Inject] private readonly LearnGameConfig _gameConfig;

        private Text _text;
        private Color _startingColor;

        private void Start()
        {
            _text = GetComponent<Text>();
            _startingColor = _text.color;
            _signalBus.Subscribe<LearnProjectSignals.ElementChosenSignal>(OnElementChosen);
            _signalBus.Subscribe<LearnProjectSignals.ElementUnChosenSignal>(OnElementUnChosen);
        }

        private void OnElementUnChosen(LearnProjectSignals.ElementUnChosenSignal obj)
        {
            _text.DOColor(new Color(_startingColor.r, _startingColor.g, _startingColor.b, 0), _gameConfig.TextFadeDuration);
        }

        private void OnElementChosen(LearnProjectSignals.ElementChosenSignal signal)
        {
            SetText(_descriptions.Descriptions[signal.Element.name]);
        }

        private void SetText(string text)
        {
            _text.DOColor(new Color(_startingColor.r, _startingColor.g, _startingColor.b, 0), 
                _gameConfig.TextFadeDuration).OnComplete(() =>
            {
                _text.text = text;
                _text.DOColor(_startingColor, _gameConfig.TextFadeDuration);
            });
        }
    }
}