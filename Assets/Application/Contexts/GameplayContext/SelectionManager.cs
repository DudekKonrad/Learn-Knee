using Application.GameplayContext.Models;
using Application.Utils;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace Application.GameplayContext
{
    public class SelectionManager : MonoBehaviour
    {
        [Inject] private readonly PlayerInputModel _playerInput;
        
        [SerializeField] private InputActionReference _leftClick;
        
        private Transform _selection;
        private ISelectionResponse _selectionResponse;
    
        private void Update()
        {
            if (_selection != null)
            {
                _selectionResponse = _selection.GetComponent<ISelectionResponse>();
                _selectionResponse?.OnDeselect();
            }

            var ray = Camera.main.ScreenPointToRay(_playerInput.Mouse);
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
            if (_leftClick.action.WasPressedThisFrame() && _selection != null)
            {
                _selection.GetComponent<ISelectionResponse>().OnClick();
            }
        }
    }
}