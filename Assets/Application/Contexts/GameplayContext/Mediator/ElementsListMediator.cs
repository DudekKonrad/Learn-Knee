using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Application.GameplayContext.Mediator
{
    public class ElementsListMediator : MonoBehaviour
    {
        [Inject] private readonly DiContainer _diContainer;
        
        [SerializeField] private GameObject _gameObjectGroup;
        [SerializeField] private GameObject _togglePrefab;
        private bool _isFirstElement = true;

        private void Start()
        {
            var children = _gameObjectGroup.GetComponentsInChildren<ModelElementView>();
            foreach (var element in children)
            {
                var toggle = _diContainer.InstantiatePrefabForComponent<ElementToggleMediator>(_togglePrefab,transform);
                toggle.ElementView = element;
                toggle.name = toggle.GetComponentInChildren<Text>().text = element.name;
                toggle.LocalizedText.SetTranslationKey(element.TranslationKey);
            }
        }
    }
}
