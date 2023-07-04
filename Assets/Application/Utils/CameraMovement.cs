using Application.GameplayContext.Models;
using Application.ProjectContext.Configs;
using DG.Tweening;
using UnityEngine;
using Zenject;

namespace Application.Utils
{
    public class CameraMovement : MonoBehaviour
    {
        [Inject] private readonly LearnGameConfig _gameConfig;
        [Inject] private readonly PlayerInputModel _playerInputModel;
        
        private void Update()
        {
            if (_playerInputModel.Scroll != 0)
            {
                var ray = transform.GetComponent<Camera>().ScreenPointToRay(_playerInputModel.Point);
                Vector3 desiredPosition;

                desiredPosition = Physics.Raycast(ray , out var hit) ? hit.point : transform.position;
                var distance = Vector3.Distance(desiredPosition , transform.position);
                var direction = Vector3.Normalize( desiredPosition - transform.position) * (distance * _playerInputModel.Scroll);
                transform.DOMove(transform.position + direction*_gameConfig.ZoomSensitivity,_gameConfig.ZoomDuration);
            }

            if (_playerInputModel.MiddleClick)
            {
                transform.position -= (Vector3)_playerInputModel.Mouse.normalized * _gameConfig.MoveSpeed;
            }
        }
    }
}
