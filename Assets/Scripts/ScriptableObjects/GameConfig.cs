using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "GameConfig", menuName = "ScriptableObjects/Configs", order = 1)]
    public class GameConfig : ScriptableObject
    {
        [SerializeField] private Color OutlineColor;
    }
}