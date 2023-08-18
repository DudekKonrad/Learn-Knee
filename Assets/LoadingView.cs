using UnityEngine;
using UnityEngine.UI;

public class LoadingView : MonoBehaviour
{
    [SerializeField] private RectTransform _holder;
    [SerializeField] private Image _circle;
    [SerializeField] [Range(0, 1)] private float _progress = 0f;

    private void Update()
    {
        _circle.fillAmount = _progress;
        _holder.rotation = Quaternion.Euler(new Vector3(0f, 0f, -_progress*360));
    }
}
    
