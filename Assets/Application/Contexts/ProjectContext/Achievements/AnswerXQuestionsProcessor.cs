using System;
using Application.ProjectContext.Signals;
using UnityEngine;
using Zenject;

namespace Application.ProjectContext.Achievements
{
    [Serializable]
    public abstract class AnswerXQuestionsProcessor
    {
        [Inject] private readonly SignalBus _signalBus;

        private int _answersCounter;
        
        [Inject]
        public void Construct()
        {
            Debug.Log($"Constructing AnswerXQuestions Processor");
            _signalBus.Subscribe<LearnProjectSignals.GameFinished>(OnGameFinishedSignal);
            _signalBus.Subscribe<LearnProjectSignals.AnswerGivenSignal>(OnAnswerGiven);
        }

        private void OnAnswerGiven(LearnProjectSignals.AnswerGivenSignal signal)
        {
            _answersCounter++;
            Debug.Log($"Given answer => counter {_answersCounter}");
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
        public int Progress => Mathf.Min(_answersCounter, Threshold);
        public float ProgressNormalized => Mathf.Clamp01(_answersCounter/Threshold);
        public bool IsCompleted => Progress >= Threshold;
        public string ProgressLabel => $"{Mathf.FloorToInt(ProgressNormalized * 100)}%";
    }
}