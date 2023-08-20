using System.Collections.Generic;
using Application.ProjectContext.Configs;
using Application.ProjectContext.Signals;
using Zenject;

namespace Application.ProjectContext.Services
{
    public enum Language
    {
        Polish = 0,
        English = 1
    }
    
    public class TranslationsService
    {
        [Inject] private readonly SignalBus _signalBus;
        [Inject] private readonly LearnGameConfig _gameConfig;
        
        private List<string[]> _data = new List<string[]>();
        private List<Language> _languages;
        private Language _selectedLanguage;
        private int _selectedLanguageIndex => _languages.IndexOf(_selectedLanguage);
        
        public Dictionary<string, Dictionary<string,string>> LanguagesDictionary { get; } = new Dictionary<string, Dictionary<string,string>>();
        public Language SelectedLanguage
        {
            get => _selectedLanguage;
            set
            {
                _selectedLanguage = value;
                _signalBus.Fire(new LearnProjectSignals.LanguageChangedSignal(_selectedLanguage));
            }
        }

        [Inject]
        private void Construct()
        {
            LoadTranslations();
            MakeDictionaries();
            _languages = new List<Language>(_gameConfig.SupportedLanguages);
        }

        public void NextLanguage()
        {
            var index = (_selectedLanguageIndex + 1) % _gameConfig.SupportedLanguages.Count;
            SelectedLanguage = _languages[index];
        }

        public void PreviousLanguage()
        {
            var index = _selectedLanguageIndex > 0
                ? _selectedLanguageIndex - 1
                : _languages.Count - 1;
            SelectedLanguage = _languages[index];
        }
        
        private void LoadTranslations()
        {
            var dataLines = _gameConfig.Translations.text.Split('\n');
            foreach (var line in dataLines)
            {
                var data = line.Split(';');
                _data.Add(data);
            }
        }

        private void MakeDictionaries()
        {
            for (var i = 0; i < _data.Count; i++)
            {
                for (var j = 0; j < _data[i].Length; j++)
                {
                    if (i == 0 && j > 0) LanguagesDictionary.Add(_data[i][j], new Dictionary<string, string>());
                    else if (j > 0)
                    {
                        LanguagesDictionary[_data[0][j]].Add(_data[i][0], _data[i][j]);
                    }
                }
            }
        }
    }
}