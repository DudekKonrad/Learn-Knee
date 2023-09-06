using Application.ProjectContext.Services;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Application.GameplayContext.Mediator.ButtonsMediators
{
    [RequireComponent(typeof(Toggle))]
    public class LatinToggleMediator : MonoBehaviour
    {
        [Inject] private TranslationsService _translationsService;

        private Toggle _toggle;

        private void Start()
        {
            _toggle = GetComponent<Toggle>();
        }

        public void ToggleLatinLanguage(bool value)
        {
            if (!_toggle.isOn) _translationsService.IsLatin = false;
            else _translationsService.IsLatin = true;
        }
    }
}
