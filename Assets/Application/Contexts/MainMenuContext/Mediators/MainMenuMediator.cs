using Application.ProjectContext.Signals;
using DG.Tweening;
using UnityEngine;
using Zenject;

namespace Application.MainMenuContext.Mediators
{
    public class MainMenuMediator : MonoBehaviour
    {
        [Inject] private readonly SignalBus _signalBus;

        [SerializeField] private GameObject _welcomePanel;
        
        private Animator _animator;

        [Inject]
        private void Construct()
        {
            _signalBus.Subscribe<LearnProjectSignals.UINavigationSignal>(OnUINavigationSignal);
            _animator = GetComponent<Animator>();
        }

        public void StartLoginPanel()
        {
            _welcomePanel.transform.DOLocalMoveY(1080, 1.2f);
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
