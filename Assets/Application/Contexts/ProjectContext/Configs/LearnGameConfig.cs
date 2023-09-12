using System.Collections.Generic;
using Application.ProjectContext.Models;
using Application.ProjectContext.Services;
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
        [SerializeField] private float _textFadeDuration;
        public Color ChooseColor => _chooseColor;
        public float ChooseColorDuration => _chooseColorDuration;
        public float MoveSpeed => _moveSpeed;
        public float RotationSpeed => _rotationSpeed;
        public float ZoomSensitivity => _zoomSensitivity;
        public float ZoomDuration => _zoomDuration;
        public float TextFadeDuration => _textFadeDuration;
        
        [Header("Quiz Mode")] [SerializeField] QuizModeConfig _quizModeConfig;
        [SerializeField] private float _pauseTime;
        [SerializeField] private float _exposeTime;
        [SerializeField] private float _calculatingResultDuration;

        public QuizModeConfig QuizModeConfig => _quizModeConfig;
        public float PauseTime => _pauseTime;
        public float ExposeTime =>_exposeTime;
        public float CalculatingResultDuration => _calculatingResultDuration;
        
        [Header("Localization")] [SerializeField] private TextAsset _descriptions;
        [SerializeField] private TextAsset _descriptionsENG;
        [SerializeField] private TextAsset _translations;
        [SerializeField] private List<Language> _supportedSupportedLanguages;

        public TextAsset Descriptions => _descriptions;
        public TextAsset DescriptionsENG => _descriptionsENG;
        public TextAsset Translations => _translations;
        public List<Language> SupportedLanguages => _supportedSupportedLanguages;
    }
}