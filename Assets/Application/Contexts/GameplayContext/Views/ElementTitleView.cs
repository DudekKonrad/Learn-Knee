using Application.ProjectContext.Configs;
using Application.ProjectContext.Services;
using Application.ProjectContext.Signals;
using Application.Utils;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Application.GameplayContext.Views
{
    public class ElementTitleView : MonoBehaviour
    {
        [Inject] private readonly SignalBus _signalBus;
        [Inject] private readonly LearnGameConfig _gameConfig;
        [Inject] private readonly TranslationsService _translationsService;
        
        [SerializeField] private LocalizedText _localizedText;
        [SerializeField] private Text _mainText;
        [SerializeField] private Text _secondaryText;
        [SerializeField] private Text _latinText;

        [Inject]
        private void Construct()
        {
            _signalBus.Subscribe<LearnProjectSignals.ElementChosenSignal>(OnElementChosenSignal);
            _signalBus.Subscribe<LearnProjectSignals.ElementUnChosenSignal>(OnElementUnChosenSignal);
        }

        private void Start()
        {
            _mainText.text = $"";
        }

        private void OnElementUnChosenSignal(LearnProjectSignals.ElementUnChosenSignal signal)
        {
            FadeText(_mainText, false);
            FadeText(_secondaryText, false);
            FadeText(_latinText, false);
        }

        private void OnElementChosenSignal(LearnProjectSignals.ElementChosenSignal signal)
        {
            _localizedText.SetTranslationKey(signal.Element.TranslationKey);
            _secondaryText.text = _translationsService.LanguagesDictionary[Language.English.ToString()][signal.Element.TranslationKey];
            _latinText.text = _translationsService.LanguagesDictionary[Language.Latin.ToString()][signal.Element.TranslationKey];
            FadeText(_mainText, true);
            FadeText(_secondaryText, true);
            FadeText(_latinText, true);
        }

        private void FadeText(Text text, bool value)
        {
            text.DOColor(value ? new Color(1, 1, 1, 1) : new Color(1, 1, 1, 0), _gameConfig.TextFadeDuration);
        }
    }
}
