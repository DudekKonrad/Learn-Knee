using System.Collections;
using System.Collections.Generic;
using Application.GameplayContext.Models;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

public class LearnPlayerMediator : MonoBehaviour
{
    [Inject] private PlayerInputModel _playerInputModel;
    private PlayerInput _playerInput;

    [Inject]
    private void Construct()
    {
        _playerInput = GetComponent<PlayerInput>();
    }

    [UsedImplicitly]
    private void OnMouse(InputValue value)
    {
        var inputValue = value.Get<Vector2>();
        _playerInputModel.Mouse = inputValue;
    }
}
