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
            _startingPosition = transform.position;
        }


        public void OnSelect()
        {
            Debug.Log($"Selected: {gameObject.name}");
            var outline = gameObject.GetComponent<Outline>();
            if (outline != null)
            {
                DOTween.To(() => outline.OutlineWidth, x => outline.OutlineWidth = x, 20, 1);
            }

            IsSelected = true;
        }

        public void OnDeselect()
        {
            var outline = gameObject.GetComponent<Outline>();
            if (outline != null)
            {
                DOTween.To(() => outline.OutlineWidth, x => outline.OutlineWidth = x, 0, 1);
            }

            IsSelected = false;
        }

        public void OnClick()
        {
            if (!IsViewed)
            {
                transform.DOMove(Camera.main.transform.position, 0.7f);
                IsViewed = true;
            }
            else
            {
                transform.DOMove(_startingPosition, 1);
                IsViewed = false;
            }
        }
    }
}