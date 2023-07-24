using System.Collections.Generic;
using Application.GameplayContext;
using UnityEngine;
using Zenject;

public class LearnModel : MonoBehaviour
{
    private readonly List<ModelElementView> _modelElements = new List<ModelElementView>();
    public List<ModelElementView> ModelElements => _modelElements;

    [Inject]
    private void Construct()
    {
        foreach (Transform child in transform)
        {
            var element = child.GetComponent<ModelElementView>();
            _modelElements.Add(element);
        }
    }
}
