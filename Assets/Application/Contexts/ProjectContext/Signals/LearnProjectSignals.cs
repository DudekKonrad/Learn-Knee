using System;
using Application.GameplayContext;
using Application.ProjectContext.Services;
using Application.QuizContext.Mediators;
using Application.QuizContext.Services;
using Application.Utils.SoundService;

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
        
        public class ElementHideSignal
        {
            public ElementHideSignal(ModelElementView element)
            {
                Element = element;
            }

            public readonly ModelElementView Element;
        }
        public class ElementIsolateSignal
        {
            public ElementIsolateSignal(ModelElementView element)
            {
                Element = element;
            }

            public readonly ModelElementView Element;
        }
        public class ShowAllElementsSignal
        {
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
            private ElementType _answerType;
            private String _answerString;
            public AnswerButtonMediator AnswerButton;
            public ConfirmButtonMediator ConfirmButton;
            public AnswerGivenSignal(ElementType elementType, AnswerButtonMediator answerButton)
            {
                _answerType = elementType;
                AnswerButton = answerButton;
            }
            public AnswerGivenSignal(ElementType elementType, ConfirmButtonMediator confirmButton)
            {
                _answerType = elementType;
                ConfirmButton = confirmButton;
            }
            
            public AnswerGivenSignal(string answer)
            {
                _answerString = answer;
            }
            
            public AnswerGivenSignal()
            {
            }


            public ElementType AnswerType => _answerType;
            public string AnswerString => _answerString;
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

        public class ChangeSoundVolume
        {
            private float _volume;
            public ChangeSoundVolume(float volume)
            {
                _volume = volume;
            }

            public float Volume => _volume;
        }
        public class ChangeMusicVolume
        {
            private float _volume;
            public ChangeMusicVolume(float volume)
            {
                _volume = volume;
            }

            public float Volume => _volume;
        }
    }
}