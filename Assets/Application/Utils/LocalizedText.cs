using Application.ProjectContext.Models;
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
            _text.text = _translationsService.LanguagesDictionary[_translationsService.SelectedLanguage.ToString()][_translationKey];
        }
    }
}
