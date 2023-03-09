using UnityEngine;

namespace Gameplay
{
    public class SelectionManager : MonoBehaviour
    {
        private Transform _selection;
        private ISelectionResponse _selectionResponse;
    
        private void Update()
        {
            if (_selection != null)
            {
                _selectionResponse = _selection.GetComponent<ISelectionResponse>();
                _selectionResponse?.OnDeselect();
            }

            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
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
            if (Input.GetMouseButtonDown(0) && _selection != null)
            {
                _selection.GetComponent<ISelectionResponse>().OnClick();
            }
        }
    }
}