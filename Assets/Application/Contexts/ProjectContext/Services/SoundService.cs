using System.Collections.Generic;
using Application.ProjectContext.Signals;
using Application.Utils.SoundService;
using DG.Tweening;
using JetBrains.Annotations;
using UnityEngine;
using Zenject;
using Object = UnityEngine.Object;

namespace Application.ProjectContext.Services
{
    public interface ISoundService
    {
        void Play(AudioClip sfxAudioClip, bool isOneShot = true, AudioSource audioSource = null, string id = "",
            bool loop = true);
        void Stop();
        void Mute(bool mute);
    }

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
            _signalBus.Subscribe<LearnProjectSignals.StopSoundSignal>(OnStopUISound);
            foreach (var s in _uiSoundConfig.SoundAudioClipsArray)
            {
                _audioClipsDictionary.Add(s._uiSounds, s.AudioClip);
            }
        }

        public void Play(AudioClip sfxAudioClip, bool isOneShot = true, AudioSource targetAudioSource = null,
            string id = "", bool loop = true)
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
                Debug.Log($"Will play clip for: {signal.UISoundsToPass}");
            }
            else
            {
                Debug.Log($"$Audio clip not found for: {signal.UISoundsToPass}");
            }
        }

        private void OnStopUISound(LearnProjectSignals.StopSoundSignal signal) => Debug.Log($"Will stop sound");
    }
}