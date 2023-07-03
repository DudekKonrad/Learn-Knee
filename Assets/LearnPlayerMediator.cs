using Application.GameplayContext.Models;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using Zenject;

[RequireComponent(typeof(PlayerInput))]
public class LearnPlayerMediator : MonoBehaviour
{
    [Inject] private PlayerInputModel _playerInputModel;

    [UsedImplicitly]
    private void OnPoint(InputValue value)
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
    private void OnScrollWheel(InputValue value)
    {
        var inputValue = value.Get<Vector2>();
        _playerInputModel.Scroll = inputValue.y;
    }    
}
