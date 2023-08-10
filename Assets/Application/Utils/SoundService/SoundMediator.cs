using Application.ProjectContext.Signals;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Application.Utils.SoundService
{
    [RequireComponent(typeof(Button))]
    public class SoundMediator : MonoBehaviour
    {
        [Inject] private SignalBus _signalBus;
        [SerializeField] private AudioClipModel.UISounds _uiSounds;
        private Button _soundButton;


        private void Start()
        {
            _soundButton = GetComponent<Button>();
            _soundButton.onClick.AddListener(PlaySound);
        }

        private void OnDestroy()
        {
            if (_soundButton != null)
            {
                _soundButton.onClick.RemoveListener(PlaySound);
            }
        }

        public void PlaySound()
        {
            _signalBus.Fire(new LearnProjectSignals.PlaySoundSignal(_uiSounds));
        }
    }
}