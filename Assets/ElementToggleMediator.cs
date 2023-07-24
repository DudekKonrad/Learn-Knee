using System;
using System.Collections;
using System.Collections.Generic;
using Application.Utils;
using UnityEngine;
using UnityEngine.UI;

public class ElementToggleMediator : MonoBehaviour
{
    private Toggle _toggle;
    private ISelectionResponse _selection;

    private void Start()
    {
        _toggle = GetComponent<Toggle>();
        _selection = GameObject.Find($"{gameObject.name}").GetComponent<ISelectionResponse>();
    }

    public void ChooseElement(bool value)
    {
        if (!_toggle.isOn) _selection.Appear();
        else _selection.Disappear();
    }
}
