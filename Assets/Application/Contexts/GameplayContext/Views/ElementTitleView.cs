using System;
using Application.ProjectContext.Configs;
using Application.ProjectContext.Signals;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Application.GameplayContext.Views
{
    [RequireComponent(typeof(Text))]
    public class ElementTitleView : MonoBehaviour
    {
        [Inject] private readonly SignalBus _signalBus;
        [Inject] private readonly LearnGameConfig _gameConfig;
        private Text _text;

        [Inject]
        private void Construct()
        {
            _signalBus.Subscribe<LearnProjectSignals.ElementChosenSignal>(OnElementChosenSignal);
            _signalBus.Subscribe<LearnProjectSignals.ElementUnChosenSignal>(OnElementUnChosenSignal);
            _text = GetComponent<Text>();
        }

        private void Start()
        {
            _text.text = $"";
        }

        private void OnElementUnChosenSignal(LearnProjectSignals.ElementUnChosenSignal signal)
        {
            FadeText(false);
        }

        private void OnElementChosenSignal(LearnProjectSignals.ElementChosenSignal signal)
        {
            _text.text = $"{signal.Element.name}";
            FadeText(true);
        }

        private void FadeText(bool value)
        {
            _text.DOColor(value ? new Color(1, 1, 1, 1) : new Color(1, 1, 1, 0), _gameConfig.TextFadeDuration);
        }
    }
}
