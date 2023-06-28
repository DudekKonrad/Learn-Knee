using DG.Tweening;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Gameplay
{
    public class CameraZoom : MonoBehaviour
    {
        [SerializeField] private float minFov = 15f;
        [SerializeField] private float maxFov = 90f;
        [SerializeField] private float sensitivity = 10f;
        [SerializeField] private InputActionReference _scroll;
        [SerializeField] private float _duration;
 
        private void Update ()
        {
            Debug.Log($"Scroll Value: {_scroll.action.ReadValue<Vector2>()}");
            var fov = Camera.main.fieldOfView;
            fov -= _scroll.action.ReadValue<Vector2>().y * sensitivity;
            fov = Mathf.Clamp(fov, minFov, maxFov);
            DOTween.To(()=> Camera.main.fieldOfView, x => Camera.main.fieldOfView = x, fov, _duration);
            //Camera.main.fieldOfView = fov;
        }
        
    }
}
