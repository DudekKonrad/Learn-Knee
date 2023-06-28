using UnityEngine;
using UnityEngine.UIElements;

public class ElementToggleMediator : MonoBehaviour
{
    [SerializeField] private Toggle _toggle;

    private void Start()
    {
        _toggle = GetComponent<Toggle>();
    }

    public void OnValueChanged(bool value)
    {
        
    }
}
