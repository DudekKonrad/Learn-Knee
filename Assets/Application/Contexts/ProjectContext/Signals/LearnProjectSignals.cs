using Application.GameplayContext;
using Application.QuizContext.Mediators;
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
            private int _correctAnswers;
            private int _incorrectAnswers;
            public GameFinished(int correctAnswers, int incorrectAnswers)
            {
                _correctAnswers = correctAnswers;
                _incorrectAnswers = incorrectAnswers;
            }

            public int CorrectAnswers => _correctAnswers;
            public int IncorrectAnswers => _incorrectAnswers;
        }
    }
}