using Application.ProjectContext.Configs;
using Application.Utils;
using DG.Tweening;
using UnityEngine;
using Zenject;

namespace Application.GameplayContext
{
    [RequireComponent(typeof(Outline))]
    public class ElementSelectionResponse : MonoBehaviour, ISelectionResponse
    {
        [Inject] private readonly LearnGameConfig _gameConfig;
        public bool IsSelected { get; set; }
        public bool IsViewed { set; get; }
        private Vector3 _startingPosition;
        private Outline _outline;

        private void Start()
        {
            _startingPosition = transform.localPosition;
            _outline = GetComponent<Outline>();
            _outline.OutlineColor = _gameConfig.OutlineColor;
        }

        public void OnSelect()
        {
            if (_outline != null)
            {
                DOTween.To(() => _outline.OutlineWidth, x => _outline.OutlineWidth = x, _gameConfig.OutlineWidth, 0.5f);
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
                DOTween.To(()=> transform.localPosition, x=> transform.localPosition = x, new Vector3(0.01f, 0.5f, 1), 1);
                IsViewed = true;
                //_description.SetActive(true);
            }
            else
            {
                DOTween.To(()=> transform.localPosition, x=> transform.localPosition = x, _startingPosition, 1);
                //_description.SetActive(false);
                IsViewed = false;
            }
        }
    }
}