using UnityEngine;
using UnityEngine.UI;

namespace Application.GameplayContext.Mediator
{
    public class ElementsListMediator : MonoBehaviour
    {
        [SerializeField] private GameObject _gameObjectGroup;
        [SerializeField] private GameObject _togglePrefab;
        private bool _isFirstElement = true;
        private void Start()
        {
            var children = _gameObjectGroup.GetComponentsInChildren<Transform>();
            foreach (var element in children)
            {
                if (!_isFirstElement)
                {
                    var toggle = Instantiate(_togglePrefab, transform);
                    toggle.name = toggle.GetComponentInChildren<Text>().text = element.name;
                }

                _isFirstElement = false;
            }
        }
    }
}
