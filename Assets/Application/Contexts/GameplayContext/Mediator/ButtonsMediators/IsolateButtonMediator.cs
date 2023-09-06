using Application.ProjectContext;
using Application.ProjectContext.Signals;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Application.GameplayContext.Mediator.ButtonsMediators
{
    public class IsolateButtonMediator : MonoBehaviour
    {
        [Inject] private readonly SignalBus _signalBus;
        [Inject] private readonly SelectedElementService _selectedElementService;

        private Button _button;

        private void Start()
        {
            _button = GetComponent<Button>();
            _button.onClick.AddListener(OnIsolate);
        }

        private void OnIsolate()
        {
            _signalBus.Fire(new LearnProjectSignals.ElementIsolateSignal(_selectedElementService.CurrentChosenModelElementView));
        }
    }
}