using Application.ProjectContext.Signals;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Application.GameplayContext.Mediator.ButtonsMediators
{
    public class ShowAllButtonMediator : MonoBehaviour
    {
        [Inject] private readonly SignalBus _signalBus;

        private Button _button;

        private void Start()
        {
            _button = GetComponent<Button>();
            _button.onClick.AddListener(OnShowAll);
        }

        private void OnShowAll()
        {
            _signalBus.Fire(new LearnProjectSignals.ShowAllElementsSignal());
        }
    }
}