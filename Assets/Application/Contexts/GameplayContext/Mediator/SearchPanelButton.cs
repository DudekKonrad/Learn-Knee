using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Application.GameplayContext.Mediator
{
    public class SearchPanelButton : MonoBehaviour
    {
        [SerializeField] private GameObject _searchPanel;
        [SerializeField] private float _duration;
        private Button _button;
        private bool _isHidden = false;
        private RectTransform _containerRect;

        private void Start()
        {
            _button = GetComponent<Button>();
            _button.onClick.AddListener(OnClick);
            _containerRect = _searchPanel.GetComponent<RectTransform>();
        }

        private void OnClick()
        {
            if (_isHidden)
            {
                _containerRect.DOLocalMoveX(_containerRect.transform.localPosition.x-400, 0.5f);
                gameObject.transform.DORotate(new Vector3(0,0,-180), _duration);
                _isHidden = false;
            }
            else
            {
                _containerRect.DOLocalMoveX(_containerRect.transform.localPosition.x+400f, 0.5f);
                gameObject.transform.DORotate(new Vector3(0,0,0), _duration);
                _isHidden = true;
            }
        }
    }
}
