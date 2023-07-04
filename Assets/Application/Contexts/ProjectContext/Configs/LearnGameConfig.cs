using UnityEngine;

namespace Application.ProjectContext.Configs
{
    [CreateAssetMenu(fileName = "LearnGameConfig", menuName = "ScriptableObjects/Configs/LearnGameConfig", order = 1)]
    public class LearnGameConfig : ScriptableObject
    {
        [Header("Selection Outline")]
        [SerializeField] private Color _outlineColor;
        [SerializeField] private float _outlineWidth;
        [Header("Free Mode")] [SerializeField] private float _rotationSpeed;
        [SerializeField] private float _moveSpeed;
        [SerializeField] private float _zoomSensitivity;
        [SerializeField] private float _zoomDuration;
        [SerializeField] private float _minZoom;
        [SerializeField] private float _maxZoom;
        public Color OutlineColor => _outlineColor;
        public float OutlineWidth => _outlineWidth;
        public float MoveSpeed => _moveSpeed;
        public float RotationSpeed => _rotationSpeed;
        public float ZoomSensitivity => _zoomSensitivity;
        public float ZoomDuration => _zoomDuration;
        public float MinZoom => _minZoom;
        public float MaxZoom => _maxZoom;

    }
}