using Application.GameplayContext.Models;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace Application.Utils
{
    public class RotateObject : MonoBehaviour
    {
        [Inject] private readonly PlayerInputModel _playerInputModel;
        [SerializeField] private GameObject _gameObject;
        [SerializeField] private float _rotationSpeed;
        [SerializeField] private InputActionReference _click;

        private void Update()
        {
            if (_click.action.IsPressed())
            {
                var xRotation = _playerInputModel.Mouse.x * _rotationSpeed;
                _gameObject.transform.rotation = Quaternion.Euler(0f, -xRotation, 0);
            }
        }

    }
}
