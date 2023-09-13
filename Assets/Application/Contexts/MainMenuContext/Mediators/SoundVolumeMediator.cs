using System;
using Application.ProjectContext.Signals;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Application.MainMenuContext.Mediators
{
    public class SoundVolumeMediator : MonoBehaviour
    {
        [Inject] private readonly SignalBus _signalBus;

        [SerializeField] private Slider _slider;

        private void Start()
        {
            _slider.value = 1f;
            _slider.onValueChanged.AddListener(OnVolumeChanged);
        }

        private void OnVolumeChanged(float volume)
        {
            _signalBus.Fire(new LearnProjectSignals.ChangeSoundVolume(volume));
        }
    }
}