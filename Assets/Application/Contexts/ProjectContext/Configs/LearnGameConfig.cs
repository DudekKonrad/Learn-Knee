using UnityEngine;

namespace Application.ProjectContext.Configs
{
    [CreateAssetMenu(fileName = "LearnGameConfig", menuName = "Configs/LearnGameConfig", order = 1)]
    public class LearnGameConfig : ScriptableObject
    {
        [Header("Element Selection")]
        [SerializeField] private Color _outlineColor;
        [SerializeField] private float _outlineWidth;
        [SerializeField] private Color _chooseColor;
        [SerializeField] private float _chooseColorDuration;
        [Header("Free Mode")] [SerializeField] private float _rotationSpeed;
        [SerializeField] private float _moveSpeed;
        [SerializeField] private float _zoomSensitivity;
        [SerializeField] private float _zoomDuration;
        [SerializeField] private float _minZoom;
        [SerializeField] private float _maxZoom;
        [SerializeField] private float _uiElementsDuration;
        public Color OutlineColor => _outlineColor;
        public float OutlineWidth => _outlineWidth;
        public Color ChooseColor => _chooseColor;
        public float ChooseColorDuration => _chooseColorDuration;
        public float MoveSpeed => _moveSpeed;
        public float RotationSpeed => _rotationSpeed;
        public float ZoomSensitivity => _zoomSensitivity;
        public float ZoomDuration => _zoomDuration;
        public float MinZoom => _minZoom;
        public float MaxZoom => _maxZoom;
        public float UiElementsDuration => _uiElementsDuration;

    }
}