using Application.ProjectContext.Configs;
using Application.ProjectContext.Models;
using Application.ProjectContext.Services;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Application.MainMenuContext.Mediators
{
    public class ChooseLanguageMediator : MonoBehaviour
    {
        [Inject] private readonly LearnGameConfig _gameConfig;
        [Inject] private readonly TranslationsService _translationsService;

        [SerializeField] private Text _text;
        private Color _startingColor;

        private void Start()
        {
            _startingColor = _text.color;
            SetText(_translationsService.SelectedLanguage.ToString());
        }

        public void NextLanguage()
        {
            _translationsService.NextLanguage();
            SetText(_translationsService.SelectedLanguage.ToString());
        }
        public void PreviousLanguage()
        {
            _translationsService.PreviousLanguage();
            SetText(_translationsService.SelectedLanguage.ToString());
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
