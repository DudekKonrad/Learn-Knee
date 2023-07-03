    using System;
    using Application.GameplayContext.Models;
using Application.ProjectContext.Configs;
using Zenject;

namespace Application.Utils
{
    using UnityEngine;
    using UnityEngine.EventSystems;

    public class RotateObject : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
    {
        [Inject] private readonly LearnGameConfig _gameConfig;
        [Inject] private readonly PlayerInputModel _playerInputModel;
        
        private bool _isDragging;
        public void OnPointerDown(PointerEventData eventData)
        {
            Debug.Log($"Pointer down");
            _isDragging = true;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            Debug.Log($"Pointer Up");
            _isDragging = false;
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (_isDragging)
            {
                Debug.Log($"Mouse drag");
                if (!_playerInputModel.RightClick) return;
                var xRotation = _playerInputModel.Mouse.x * _gameConfig.RotationSpeed;
                var yRotation = _playerInputModel.Mouse.y * _gameConfig.RotationSpeed; 
                
                gameObject.transform.Rotate(Vector3.down, xRotation);
                gameObject.transform.Rotate(Vector3.right, yRotation);  
            }
        }
    }
}
