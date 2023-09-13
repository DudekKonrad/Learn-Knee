using System.Collections.Generic;
using Application.ProjectContext.Configs;
using Application.ProjectContext.Signals;
using Application.Utils.SoundService;
using DG.Tweening;
using JetBrains.Annotations;
using UnityEngine;
using Zenject;
using Object = UnityEngine.Object;

namespace Application.ProjectContext.Services
{
    [UsedImplicitly]
    public class SoundService : ISoundService
    {
        [Inject] private readonly SignalBus _signalBus;
        [Inject] private readonly UISoundConfig _uiSoundConfig;


        private readonly Dictionary<AudioClipModel.UISounds, AudioClip> _audioClipsDictionary =
            new Dictionary<AudioClipModel.UISounds, AudioClip>();

        public AudioClip this[AudioClipModel.UISounds uiSounds] => _audioClipsDictionary[uiSounds];
        private AudioSource _audioSourceSfx;


        [Inject]
        public void Construct()
        {
            _audioSourceSfx = Object.Instantiate(new GameObject()).AddComponent<AudioSource>();
            Object.DontDestroyOnLoad(_audioSourceSfx.gameObject);
            _signalBus.Subscribe<LearnProjectSignals.PlaySoundSignal>(OnPlayUISound);
            _signalBus.Subscribe<LearnProjectSignals.ChangeMusicVolume>(OnChangeMusicVolume);
            _signalBus.Subscribe<LearnProjectSignals.ChangeSoundVolume>(OnChangeSoundVolume);
            foreach (var s in _uiSoundConfig.SoundAudioClipsArray)
            {
                _audioClipsDictionary.Add(s._uiSounds, s.AudioClip);
            }
        }

        private void OnChangeSoundVolume(LearnProjectSignals.ChangeSoundVolume signal)
        {
            _audioSourceSfx.volume = signal.Volume;
        }

        private void OnChangeMusicVolume(LearnProjectSignals.ChangeMusicVolume signal)
        {
            throw new System.NotImplementedException();
        }

        public void Play(AudioClip sfxAudioClip, bool isOneShot = true)
        {
            if (isOneShot) _audioSourceSfx.PlayOneShot(sfxAudioClip);
            else
            {
                if (_audioSourceSfx.isPlaying == false || _audioSourceSfx.clip != sfxAudioClip)
                {
                    _audioSourceSfx.clip = sfxAudioClip;
                    _audioSourceSfx.Play();
                }
            }
        }

        public void Stop()
        {
            _audioSourceSfx.Stop();
        }

        public void Mute(bool mute)
        {
            var sequence = DOTween.Sequence();
            if (mute)
            {
                if (_audioSourceSfx.volume >= 0)
                {
                    var fadeOut = _audioSourceSfx.DOFade(0f, .6f);
                    sequence.Insert(0f, fadeOut);
                }
            }
            else
            {
                _audioSourceSfx.volume = 0f;
                var fadeIn = _audioSourceSfx.DOFade(1f, .6f);
                sequence.Insert(0f, fadeIn);
            }
            sequence.Play();
        }

        private void OnPlayUISound(LearnProjectSignals.PlaySoundSignal signal)
        {
            if (_audioClipsDictionary.TryGetValue(signal.UISoundsToPass, out var audioClip))
            {
                Play(audioClip, signal.IsOneShot);
            }
            else
            {
                Debug.Log($"Audio clip not found for: {signal.UISoundsToPass}");
            }
        }
    }
}