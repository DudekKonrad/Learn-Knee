using System;
using Application.ProjectContext.Services;
using Application.ProjectContext.Signals;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Application.Utils
{
    [RequireComponent(typeof(Text))]
    public class LocalizedText : MonoBehaviour
    {
        [Inject] private readonly SignalBus _signalBus;
        [Inject] private TranslationsService _translationsService;

        [SerializeField] private string _translationKey;
    
        [SerializeField] private Text _text;

        [Inject]
        private void Construct()
        {
            _signalBus.Subscribe<LearnProjectSignals.LanguageChangedSignal>(OnLanguageChanged);
            _text = GetComponent<Text>();
        }

        private void Start()
        {
            UpdateLanguage();
        }

        public void SetTranslationKey(string key)
        {
            _translationKey = key;
            UpdateLanguage();
        }

        private void OnLanguageChanged(LearnProjectSignals.LanguageChangedSignal signal)
        {
            UpdateLanguage();
        }

        private void UpdateLanguage()
        {
            if (_translationsService.IsLatin)
            {
                if (_translationsService.LanguagesDictionary[Language.Latin.ToString()].ContainsKey(_translationKey)
                    && _translationsService.LanguagesDictionary[Language.Latin.ToString()][_translationKey] != String.Empty)
                {
                    _text.text = _translationsService.LanguagesDictionary[Language.Latin.ToString()][_translationKey];
                    return;
                }
            }
            if (!_translationsService.LanguagesDictionary[_translationsService.SelectedLanguage.ToString()].ContainsKey(_translationKey))
            {
                Debug.Log($"{gameObject.name} -> Missing translation for: {_translationKey}");
                _text.text = $"NO TRANSLATION";
            }
            else
            {
                _text.text = _translationsService.LanguagesDictionary[_translationsService.SelectedLanguage.ToString()][_translationKey];
            }
        }
    }
}
