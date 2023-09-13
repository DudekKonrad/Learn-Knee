using Application.GameplayContext;
using Application.ProjectContext.Signals;
using JetBrains.Annotations;
using Zenject;

namespace Application.ProjectContext
{
    [UsedImplicitly]
    public class SelectedElementService
    {
        [Inject] private readonly SignalBus _signalBus;
        private ModelElementView _currentChosenModelElementView;
        public ModelElementView CurrentChosenModelElementView => _currentChosenModelElementView;

        [Inject]
        private void Construct()
        {
            _signalBus.Subscribe<LearnProjectSignals.ElementChosenSignal>(OnElementChosenSignal);
        }

        private void OnElementChosenSignal(LearnProjectSignals.ElementChosenSignal signal)
        {
            _currentChosenModelElementView = signal.Element;
        }
    }
}