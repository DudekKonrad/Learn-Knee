using DG.Tweening;
using UnityEngine;

namespace Gameplay
{
    public class ElementSelectionResponse : MonoBehaviour, ISelectionResponse
    {
        public bool IsSelected { get; set; }
        public bool IsViewed { set; get; }
        private Vector3 _startingPosition;

        private void Start()
        {
            _startingPosition = transform.localPosition;
        }


        public void OnSelect()
        {
            Debug.Log($"Selected: {gameObject.name}");
            var outline = gameObject.GetComponent<Outline>();
            if (outline != null)
            {
                DOTween.To(() => outline.OutlineWidth, x => outline.OutlineWidth = x, 10, 0.5f);
            }
            IsSelected = true;
        }

        public void OnDeselect()
        {
            var outline = gameObject.GetComponent<Outline>();
            if (outline != null)
            {
                DOTween.To(() => outline.OutlineWidth, x => outline.OutlineWidth = x, 0, 0.5f);
            }

            IsSelected = false;
        }

        public void OnClick()
        {
            if (!IsViewed)
            {
                var toCameraPosition = Vector3.MoveTowards(transform.localPosition, Camera.main.transform.position, 10);
                Debug.Log($"Cliked: {name} going to: {toCameraPosition}");
                DOTween.To(()=> transform.localPosition, x=> transform.localPosition = x, new Vector3(0.01f, 1, 1), 1);
                //transform.localPosition = toCameraPosition;//DOMove(toCameraPosition, 0.7f);
                IsViewed = true;
            }
            else
            {
                Debug.Log($"Clicked: {name} going back to: {_startingPosition}");
                DOTween.To(()=> transform.localPosition, x=> transform.localPosition = x, _startingPosition, 1);
                //transform.localPosition = _startingPosition;//DOMove(_startingPosition, 0.7f);
                IsViewed = false;
            }
        }
    }
}