using System;
using Application.ProjectContext.Configs;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class SearchElementsMediator : MonoBehaviour
{
    [Inject] private readonly LearnGameConfig _gameConfig;
    
    [SerializeField] private InputField _inputField;
    [SerializeField] private GameObject _elementsContainer;
    [SerializeField] private float _test;
    private float _elementsContainerStartingY;

    private void Start()
    {
        _inputField.onValueChanged.AddListener(OnValueChanged);
        foreach (Transform element in _elementsContainer.transform)
        {
            Debug.Log($"Element: {element.name}");
        }
    }

    private void OnValueChanged(string value)
    {
        Debug.Log($"Value changed to: {value}");
        foreach (Transform element in _elementsContainer.transform)
        {
            element.gameObject.SetActive(element.name.Contains(value));
        }
    }
}
