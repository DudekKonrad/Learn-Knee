using Application.GameplayContext;
using Application.ProjectContext;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

[RequireComponent(typeof(Button))]
public class IsolateButtonMediator : MonoBehaviour
{
    [Inject] private readonly LearnModel _learnModel;
    [Inject] private readonly SelectedElementService _selectedElementService;
    [SerializeField] private SelectionManager _selectionManager;
    
    private Button _button;

    private void Start()
    {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(OnClick);
    }

    private void OnClick()
    {
        IsolateElement();
    }

    private void IsolateElement()
    {
        foreach (var element in _selectionManager.LearnModelElements)
        {
            if (element.name != _selectedElementService.CurrentChosenModelElementView.name)
            {
                Debug.Log($"Isolate element: {element.gameObject.name}");
                element.gameObject.SetActive(false);
            }
        }
    }
}
