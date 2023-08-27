using Application.Utils;
using UnityEngine;
using UnityEngine.UI;

namespace Application.GameplayContext.Mediator
{
    public class SearchElementsMediator : MonoBehaviour
    {
        [SerializeField] private InputField _inputField;
        [SerializeField] private GameObject _elementsContainer;
        private float _elementsContainerStartingY;

        private void Start()
        {
            _inputField.onValueChanged.AddListener(OnValueChanged);
        }

        private void OnValueChanged(string value)
        {
            foreach (Transform element in _elementsContainer.transform)
            {
                var nameNormalized = StringExtensionMethods.NormalizeString(element.name.ToLower());
                var valueNormalized = StringExtensionMethods.NormalizeString(value.ToLower());
                element.gameObject.SetActive(nameNormalized.Contains(valueNormalized));
            }
        }
    }
}
