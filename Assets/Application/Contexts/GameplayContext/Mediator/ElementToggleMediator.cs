using Application.ProjectContext.Signals;
using Application.Utils;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Application.GameplayContext.Mediator
{
    public class ElementToggleMediator : MonoBehaviour
    {
        [Inject] private readonly SignalBus _signalBus;

        private Toggle _toggle;
        public ModelElementView ElementView;
        private SelectionManager _selectionManager;
        public LocalizedText LocalizedText;
        
        [Inject]
        private void Construct()
        {
            _signalBus.Subscribe<LearnProjectSignals.ElementIsolateSignal>(OnElementIsolateSignal);
            _signalBus.Subscribe<LearnProjectSignals.ElementHideSignal>(OnElementHideSignal);
            _signalBus.Subscribe<LearnProjectSignals.ShowAllElementsSignal>(OnShowAllElementsSignal);
        }

        private void OnShowAllElementsSignal(LearnProjectSignals.ShowAllElementsSignal signal)
        {
            _toggle.isOn = false;
        }

        private void OnElementHideSignal(LearnProjectSignals.ElementHideSignal signal)
        {
            if (signal.Element.ElementType == ElementView.ElementType) _toggle.isOn = true;
        }

        private void OnElementIsolateSignal(LearnProjectSignals.ElementIsolateSignal signal)
        {
            if (signal.Element.ElementType != ElementView.ElementType) _toggle.isOn = true;
        }

        private void Start()
        {
            _toggle = GetComponent<Toggle>();
            _toggle.onValueChanged.AddListener(ChooseElement);
        }

        private void ChooseElement(bool value)
        {
            if (!_toggle.isOn) ElementView.Appear();
            else ElementView.Disappear();
        }
    }
}
