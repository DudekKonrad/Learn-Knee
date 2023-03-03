using DG.Tweening;
using UnityEngine;

public class OutlineSelectionResponse : MonoBehaviour, ISelectionResponse
{
    public void OnSelect()
    {
        var outline = gameObject.GetComponent<Outline>();
        if (outline != null)
        {
            DOTween.To(() => outline.OutlineWidth, x => outline.OutlineWidth = x, 20, 1);
            //outline.OutlineWidth = 10;
        }
    }

    public void OnDeselect()
    {
        var outline = gameObject.GetComponent<Outline>();
        if (outline != null)
        {
            DOTween.To(() => outline.OutlineWidth, x => outline.OutlineWidth = x, 0, 1);

            //outline.OutlineWidth = 0;
        }
    }
}