using Zenject;

namespace Application.QuizContext.Models
{
    public class PlayerLearnRuntimeModel
    {
        [Inject] private readonly SignalBus _signalBus;
    }
}