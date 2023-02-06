using UnityEngine;

public class SelectionManager : MonoBehaviour
{
    private Transform _selection;
    private ISelectionResponse _selectionResponse;
    
    private void Update()
    {
        if (_selection != null)
        {
            _selectionResponse = _selection.GetComponent<ISelectionResponse>();
            if (_selectionResponse != null)
            {
                _selectionResponse.OnDeselect();   
            }
        }

        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        _selection = null;
        if (Physics.Raycast(ray, out var hit))
        {
            var selection = hit.transform;
            if (selection.gameObject.GetComponent<ISelectionResponse>() != null)
            {
                Debug.Log($"Got selection");
                _selection = selection;
            }
        }

        if (_selection != null)
        {
            _selectionResponse = _selection.GetComponent<ISelectionResponse>();
            if (_selectionResponse != null)
            {
                _selectionResponse.OnSelect();   
            }
        }
    }
}