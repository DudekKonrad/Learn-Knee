using Application.GameplayContext.Models;
using Application.ProjectContext.Signals;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace Application.ProjectContext.Mediators
{
    [RequireComponent(typeof(PlayerInput))]
    public class LearnPlayerMediator : MonoBehaviour
    {
        [Inject] private SignalBus _signalBus;
        [Inject] private PlayerInputModel _playerInputModel;

        [UsedImplicitly]
        private void OnPoint(InputValue value)
        {
            var inputValue = value.Get<Vector2>();
            _playerInputModel.Point = inputValue;
        }
    
        [UsedImplicitly]
        private void OnMouse(InputValue value)
        {
            var inputValue = value.Get<Vector2>();
            _playerInputModel.Mouse = inputValue;
        }
    
        [UsedImplicitly]
        private void OnRightClick(InputValue value)
        {
            _playerInputModel.RightClick = value.isPressed;
        }
    
        [UsedImplicitly]
        private void OnMiddleClick(InputValue value)
        {
            _playerInputModel.MiddleClick = value.isPressed;
        }
    
        [UsedImplicitly]
        private void OnScrollWheel(InputValue value)
        {
            var inputValue = value.Get<Vector2>();
            _playerInputModel.Scroll = inputValue.y;
        }

        [UsedImplicitly]
        private void OnBack(InputValue value)
        {
            _signalBus.Fire(new LearnProjectSignals.UINavigationSignal("Back"));
        }  
    }
}
