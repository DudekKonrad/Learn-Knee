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
    }
}