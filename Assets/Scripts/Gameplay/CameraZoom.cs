using UnityEngine;

namespace Gameplay
{
    public class CameraZoom : MonoBehaviour
    {
        [SerializeField] private float minFov = 15f;
        [SerializeField] private float maxFov = 90f;
        [SerializeField] private float sensitivity = 10f;
 
        private void Update () {
            var fov = Camera.main.fieldOfView;
            fov -= Input.GetAxis("Mouse ScrollWheel") * sensitivity;
            fov = Mathf.Clamp(fov, minFov, maxFov);
            Camera.main.fieldOfView = fov;
        }
    }
}
