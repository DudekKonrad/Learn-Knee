using UnityEngine;

namespace Application.ProjectContext.Configs
{
    [CreateAssetMenu(fileName = "LearnGameConfig", menuName = "ScriptableObjects/Configs/LearnGameConfig", order = 1)]
    public class LearnGameConfig : ScriptableObject
    {
        [Header("Selection Outline")]
        [SerializeField] private Color _outlineColor;
        [SerializeField] private float _outlineWidth;
        public Color OutlineColor => _outlineColor;
        public float OutlineWidth => _outlineWidth;
    }
}