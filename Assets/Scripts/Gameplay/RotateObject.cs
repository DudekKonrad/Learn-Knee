using UnityEngine;
using UnityEngine.InputSystem;

namespace Gameplay
{
    public class RotateObject : MonoBehaviour
    {
        [SerializeField] private GameObject _gameObject;
        [SerializeField] private float _rotationSpeed;
        [SerializeField] private InputActionReference _mouse;
        [SerializeField] private InputActionReference _click;

        private void Update()
        {
            if (_click.action.IsPressed())
            {
                var xRotation = _mouse.action.ReadValue<Vector2>().x * _rotationSpeed;
                _gameObject.transform.rotation = Quaternion.Euler(0f, -xRotation, 0);
            }
        }

    }
}
