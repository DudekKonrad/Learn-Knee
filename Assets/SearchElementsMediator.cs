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
        _elementsContainerStartingY = _elementsContainer.transform.position.y;
    }

    private void Update()
    {
        if (_inputField.isFocused)
        {
            _elementsContainer.transform.DOMoveY(_test, _gameConfig.UiElementsDuration);
        }
        else
        {
            _elementsContainer.transform.DOMoveY(_elementsContainerStartingY, _gameConfig.UiElementsDuration);
        }
    }
}
