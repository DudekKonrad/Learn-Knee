﻿using System;
using Application.GameplayContext;
using Application.ProjectContext.Achievements;
using Application.ProjectContext.Models;
using Application.ProjectContext.Services;
using Application.QuizContext.Mediators;
using Application.QuizContext.Services;
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
        public class AnswerGivenSignal
        {
            private string _answer;
            public AnswerButtonMediator AnswerButton;
            public ConfirmButtonMediator ConfirmButton;
            public AnswerGivenSignal(string answer, AnswerButtonMediator answerButton)
            {
                _answer = answer;
                AnswerButton = answerButton;
            }
            public AnswerGivenSignal(string answer, ConfirmButtonMediator confirmButton)
            {
                _answer = answer;
                ConfirmButton = confirmButton;
            }
            
            public AnswerGivenSignal(string answer)
            {
                _answer = answer;
            }
            
            public AnswerGivenSignal()
            {
            }


            public string Answer => _answer;
        }

        public class GameFinished
        {
            private GameResult _gameResult;

            public GameFinished(GameResult gameResult)
            {
                _gameResult = gameResult;
            }
            
            public GameResult GameResult => _gameResult;
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
        
        public class LanguageChangedSignal
        {
            private readonly Language _language;

            public LanguageChangedSignal(Language language)
            {
                _language = language;
            }

            public Language Language => _language;
        }
        public class LoadSceneSignal
        {
            private readonly string _sceneName;

            public LoadSceneSignal(string sceneName)
            {
                _sceneName = sceneName;
            }

            public string SceneName => _sceneName;
        }
    }
}