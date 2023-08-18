using System;
using Application.ProjectContext.Models;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

[RequireComponent(typeof(Button))]
public class PreviousLanguageButtonMediator : MonoBehaviour
{
    [Inject] private readonly TranslationsService _translationsService;

    private Button _button;

    private void Start()
    {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(OnClick);
    }

    private void OnClick()
    {
        _translationsService.PreviousLanguage();
    }
}
