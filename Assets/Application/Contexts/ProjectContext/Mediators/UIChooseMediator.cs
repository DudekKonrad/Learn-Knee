using Application.ProjectContext.Signals;
using Application.Utils.SoundService;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Application.ProjectContext.Mediators
{
    [RequireComponent(typeof(Button))]
    public class UIChooseMediator : MonoBehaviour
    {
        [Inject] private readonly SignalBus _signalBus;
        [SerializeField] private string _chooseTrigger;

        private Button _button;
        private void Start()
        {
            _button = GetComponent<Button>();
            _button.onClick.AddListener(OnChoose);
        }

        private void OnChoose()
        {
            Debug.Log($"Choose {_chooseTrigger}");
            _signalBus.Fire(new LearnProjectSignals.PlaySoundSignal(AudioClipModel.UISounds.OnChoose));
            _signalBus.Fire(new LearnProjectSignals.UINavigationSignal(_chooseTrigger));
        }
    }
}