using System.Collections.Generic;
using Gameplay;
using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "ElementsConfig", menuName = "ScriptableObjects/Configs", order = 1)]
    public class ElementsConfig : ScriptableObject
    {
        [SerializeField] private List<ISelectableElement> _selectableElements;
    }
}