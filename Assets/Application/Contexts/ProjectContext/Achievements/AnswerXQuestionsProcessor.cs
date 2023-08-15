using System;
using Application.ProjectContext.Signals;
using UnityEngine;
using Zenject;

namespace Application.ProjectContext.Achievements
{
    [Serializable]
    public abstract class AnswerXQuestionsProcessor : IAchievementProcessor, IInitializable
    {
        [Inject] private readonly SignalBus _signalBus;
        
        public void Initialize()
        {
            _signalBus.Subscribe<LearnProjectSignals.GameFinished>(OnGameFinishedSignal);
        }

        private void OnGameFinishedSignal(LearnProjectSignals.GameFinished signal)
        {
            if (IsCompleted) return;
            if (IsCompleted)
            {
                _signalBus.Fire(new LearnProjectSignals.AchievementUnlockedSignal(AchievementType));
            }
        }

        public abstract int Threshold { get; }
        public abstract Enum AchievementType { get; }
        public int Progress => Mathf.Min(10, Threshold);
        public float ProgressNormalized => Mathf.Clamp01(10/Threshold);
        public bool ShowProgress => true;
        public bool IsCompleted => Progress >= Threshold;
        public string ProgressLabel => $"{Mathf.FloorToInt(ProgressNormalized * 100)}%";
    }
}