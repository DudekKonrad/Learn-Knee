using Application.ProjectContext.Signals;
using UnityEngine;
using Zenject;

namespace Application.ProjectContext.Achievements.Mediators
{
    public class LoadingScreenMediator : MonoBehaviour
    {
        [Inject] private readonly SignalBus _signalBus;

        [SerializeField] private Animator _animator;
        private static readonly int IsVisible = Animator.StringToHash("IsVisible");

        private void Awake()
        {
            transform.SetParent(null);
            DontDestroyOnLoad(gameObject);
        }

        private void Start()
        {
            _signalBus.Subscribe<LearnProjectSignals.ShowLoadingScreenSignal>(OnShowLoadingScreenSignal);
            _signalBus.Subscribe<LearnProjectSignals.HideLoadingScreenSignal>(OnHideLoadingScreenSignal);
        }

        private void OnHideLoadingScreenSignal(LearnProjectSignals.HideLoadingScreenSignal signal)
        {
            _animator.SetBool(IsVisible, false);
        }

        private void OnShowLoadingScreenSignal(LearnProjectSignals.ShowLoadingScreenSignal signal)
        {
            _animator.SetBool(IsVisible, true);
        }
    }
}