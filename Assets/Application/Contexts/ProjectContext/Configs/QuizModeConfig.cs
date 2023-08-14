using System;
using UnityEngine;

namespace Application.ProjectContext.Configs
{
    [Serializable]
    public class QuizModeConfig
    {
        [SerializeField] private float _duration;

        public float Duration => _duration;
    }
}