using Application.GameplayContext;

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
    }
}