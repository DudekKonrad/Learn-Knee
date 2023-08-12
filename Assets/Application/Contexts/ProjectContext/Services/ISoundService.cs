using UnityEngine;

namespace Application.ProjectContext.Services
{
    public interface ISoundService
    {
        void Play(AudioClip sfxAudioClip, bool isOneShot = true);
        void Stop();
        void Mute(bool mute);
    }
}