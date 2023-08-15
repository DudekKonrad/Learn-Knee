﻿using System;
using Application.GameplayContext;
using Application.ProjectContext.Achievements;
using Application.QuizContext.Mediators;
using Application.Utils.SoundService;
using JetBrains.Annotations;
using UnityEngine.UI;

namespace Application.ProjectContext.Signals
{
    public class LearnProjectSignals
    {
        public class ElementChosenSignal
        {
            public ElementChosenSignal(ModelElementView element)
            {
                Element = element;
            }

            public readonly ModelElementView Element;
        }
        
        public class ElementUnChosenSignal
        {
            public ElementUnChosenSignal(ModelElementView element)
            {
                Element = element;
            }

            public readonly ModelElementView Element;
        }
        
        public class UINavigationSignal
        {
            public UINavigationSignal(string trigger)
            {
                Trigger = trigger;
            }

            public string Trigger { get; }
        }

        public class TimeIsUpSignal
        {
            
        }
        public class AnswerGivenSignal
        {
            private string _answer;
            public AnswerButtonMediator Button;
            public AnswerGivenSignal(string answer, AnswerButtonMediator button)
            {
                _answer = answer;
                Button = button;
            }

            public string Answer => _answer;
        }

        public class GameFinished
        {
            private readonly int _correctAnswers;
            private readonly int _incorrectAnswers;
            public GameFinished(int correctAnswers, int incorrectAnswers)
            {
                _correctAnswers = correctAnswers;
                _incorrectAnswers = incorrectAnswers;
            }

            public int CorrectAnswers => _correctAnswers;
            public int IncorrectAnswers => _incorrectAnswers;
        }
        
        public class PlaySoundSignal
        {
            public PlaySoundSignal(AudioClipModel.UISounds uiSoundsToPass, bool isOneShot = true)
            {
                UISoundsToPass = uiSoundsToPass;
                IsOneShot = isOneShot;
            }

            public readonly AudioClipModel.UISounds UISoundsToPass;
            public readonly bool IsOneShot;
        }
        
        public class StopSoundSignal
        {
            public StopSoundSignal(AudioClipModel.UISounds uiSoundsToPass)
            {
                UISoundsToPass = uiSoundsToPass;
            }

            public readonly AudioClipModel.UISounds UISoundsToPass;
        }
        public class AchievementUnlockedSignal
        {
            public readonly Enum AchievementType;
            public readonly int AchievementTypeId;

            public AchievementUnlockedSignal(Enum achievementType)
            {
                AchievementType = achievementType;
                AchievementTypeId = Convert.ToInt32(achievementType);
            }
        }
    }
}