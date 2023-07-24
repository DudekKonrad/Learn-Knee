using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class SearchPanelButton : MonoBehaviour
{
    [SerializeField] private GameObject _searchPanel;
    [SerializeField] private Text _text;
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
            Debug.Log($"Show");
            _containerRect.DOLocalMoveX(_containerRect.transform.localPosition.x-400, 0.5f);
            _text.text = $">";
            _isHidden = false;
        }
        else
        {
            Debug.Log($"Hide");
            _containerRect.DOLocalMoveX(_containerRect.transform.localPosition.x+400f, 0.5f);
            _text.text = $"<";
            _isHidden = true;
        }
    }
}
