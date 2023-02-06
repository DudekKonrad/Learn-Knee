using UnityEngine;

public class OutlineSelectionResponse : MonoBehaviour, ISelectionResponse
{
    public void OnSelect()
    {
        var outline = gameObject.GetComponent<Outline>();
        if (outline != null) outline.OutlineWidth = 10;
    }

    public void OnDeselect()
    {
        var outline = gameObject.GetComponent<Outline>();
        if (outline != null) outline.OutlineWidth = 0;
    }
}