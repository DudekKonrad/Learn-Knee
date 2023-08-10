using Application.Utils.SoundService;
using UnityEngine;

namespace Application.ProjectContext.Services
{
    [CreateAssetMenu(menuName = "Configs/Create SoundConfig", fileName = "SoundConfig", order = 0)]
    public class UISoundConfig : ScriptableObject
    {
        [SerializeField] private AudioClipModel[] _soundAudioClipsArray = {};

        public AudioClipModel[] SoundAudioClipsArray => _soundAudioClipsArray;
    }
    
}