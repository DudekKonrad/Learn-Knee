using UnityEngine;
using UnityEngine.UI;

public class ElementsListMediator : MonoBehaviour
{
    [SerializeField] private GameObject _gameObjectGroup;
    [SerializeField] private GameObject _togglesGroup;
    [SerializeField] private GameObject _togglePrefab;
    private void Start()
    {
        var children = _gameObjectGroup.GetComponentsInChildren<Transform>();
        foreach (var element in children)
        {
            var toggle = Instantiate(_togglePrefab, _togglesGroup.transform);
            toggle.name = toggle.GetComponentInChildren<Text>().text = element.name;
        }
    }
}
