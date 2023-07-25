using Application.ProjectContext.Signals;
using UnityEngine;
using Zenject;

namespace Application.MainMenuContext.Mediators
{
    public class MainMenuMediator : MonoBehaviour
    {
        [Inject] private readonly SignalBus _signalBus;
        private Animator _animator;

        [Inject]
        private void Construct()
        {
            _signalBus.Subscribe<LearnProjectSignals.UINavigationSignal>(OnUINavigationSignal);
            _animator = GetComponent<Animator>();
        }

        private void OnUINavigationSignal(LearnProjectSignals.UINavigationSignal signal)
        {
            _animator.SetTrigger(signal.Trigger);
        }

        public void OnExitButtonClick()
        {
            UnityEngine.Application.Quit();
        }
    }
}
