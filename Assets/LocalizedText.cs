using Application.ProjectContext.Models;
using Application.ProjectContext.Signals;
using UnityEngine;
using UnityEngine.UI;
using Zenject;


[RequireComponent(typeof(Text))]
public class LocalizedText : MonoBehaviour
{
    [Inject] private readonly SignalBus _signalBus;
    [Inject] private TranslationsService _translationsService;

    [SerializeField] private string _translationKey;
    
    private Text _text;

    [Inject]
    private void Construct()
    {
        _signalBus.Subscribe<LearnProjectSignals.LanguageChangedSignal>(OnLanguageChanged);
    }
    

    private void OnLanguageChanged(LearnProjectSignals.LanguageChangedSignal signal)
    {
        Debug.Log($"Localized text for {_translationKey}");
        Debug.Log($"Language changed to: {_translationsService.SelectedLanguage.ToString()} new value = " +
                  $"{_translationsService.LanguagesDictionary[_translationsService.SelectedLanguage.ToString()][_translationKey]}");
        UpdateLanguage();
    }

    private void Start()
    {
        _text = GetComponent<Text>();
        UpdateLanguage();
    }

    private void UpdateLanguage()
    {
        _text.text = _translationsService.LanguagesDictionary[_translationsService.SelectedLanguage.ToString()][_translationKey];
    }
}
