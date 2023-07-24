using Application.GameplayContext.Models;
using Application.ProjectContext.Configs;
using UnityEngine;
using Zenject;

namespace Application.Utils
{
    public class RotateObject : MonoBehaviour
    {
        [Inject] private readonly LearnGameConfig _gameConfig;
        [Inject] private readonly PlayerInputModel _playerInputModel;
        
        public void Update()
        {
            if (_playerInputModel.RightClick)
            {
                var xRotation = _playerInputModel.Mouse.x * _gameConfig.RotationSpeed;
                var yRotation = _playerInputModel.Mouse.y * _gameConfig.RotationSpeed;
                transform.Rotate(Vector3.up, -xRotation, Space.World); 
                transform.Rotate(-Vector3.right, -yRotation, Space.World);
            }
        }

    }
}
