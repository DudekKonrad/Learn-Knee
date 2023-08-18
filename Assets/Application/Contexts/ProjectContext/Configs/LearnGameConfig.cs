using System.Collections.Generic;
using Application.ProjectContext.Models;
using UnityEngine;

namespace Application.ProjectContext.Configs
{
    [CreateAssetMenu(fileName = "LearnGameConfig", menuName = "Configs/LearnGameConfig", order = 1)]
    public class LearnGameConfig : ScriptableObject
    {
        [Header("Element Selection")]
        [SerializeField] private Color _chooseColor;
        [SerializeField] private float _chooseColorDuration;
        [Header("Free Mode")] [SerializeField] private float _rotationSpeed;
        [SerializeField] private float _moveSpeed;
        [SerializeField] private float _zoomSensitivity;
        [SerializeField] private float _zoomDuration;
        [SerializeField] private float _minZoom;
        [SerializeField] private float _maxZoom;
        [SerializeField] private float _textFadeDuration;
        public Color ChooseColor => _chooseColor;
        public float ChooseColorDuration => _chooseColorDuration;
        public float MoveSpeed => _moveSpeed;
        public float RotationSpeed => _rotationSpeed;
        public float ZoomSensitivity => _zoomSensitivity;
        public float ZoomDuration => _zoomDuration;
        public float MinZoom => _minZoom;
        public float MaxZoom => _maxZoom;
        public float TextFadeDuration => _textFadeDuration;
        
        [Header("Quiz Mode")] [SerializeField] QuizModeConfig _quizModeConfig;
        [SerializeField] private int _numberOfClosestElementsToShow;
        [SerializeField] private float _pauseTime;

        public QuizModeConfig QuizModeConfig => _quizModeConfig;
        public int NumberOfClosestElementsToShow => _numberOfClosestElementsToShow;
        public float PauseTime => _pauseTime;

        [Header("Localization")] [SerializeField] private TextAsset _descriptions;
        [SerializeField] private TextAsset _translations;
        [SerializeField] private List<Language> _supportedSupportedLanguages;

        public TextAsset Descriptions => _descriptions;
        public TextAsset Translations => _translations;
        public List<Language> SupportedLanguages => _supportedSupportedLanguages;


    }
}