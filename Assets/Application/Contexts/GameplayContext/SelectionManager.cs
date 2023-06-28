using Application.Utils;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Application.GameplayContext
{
    public class SelectionManager : MonoBehaviour
    {
        [SerializeField] private InputActionReference _mouse;
        [SerializeField] private InputActionReference _click;
        private Transform _selection;
        private ISelectionResponse _selectionResponse;
    
        private void Update()
        {
            if (_selection != null)
            {
                _selectionResponse = _selection.GetComponent<ISelectionResponse>();
                _selectionResponse?.OnDeselect();
            }

            var ray = Camera.main.ScreenPointToRay(_mouse.action.ReadValue<Vector2>());
            _selection = null;
            if (Physics.Raycast(ray, out var hit))
            {
                var selection = hit.transform;
                var selectionResponse = selection.gameObject.GetComponent<ISelectionResponse>();
                if ( selectionResponse != null)
                {
                    _selection = selection;
                }
            }
            MouseClick();

            if (_selection != null)
            {
                _selectionResponse = _selection.GetComponent<ISelectionResponse>();
                _selectionResponse?.OnSelect();
            }
        }

        private void MouseClick()
        {
            if (_click.action.WasPressedThisFrame() && _selection != null)
            {
                _selection.GetComponent<ISelectionResponse>().OnClick();
            }
        }
    }
}