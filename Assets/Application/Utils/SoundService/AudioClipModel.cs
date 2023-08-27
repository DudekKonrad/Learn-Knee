using System;
using UnityEngine;

namespace Application.Utils.SoundService
{
    [Serializable]
    public class AudioClipModel
    {
        public enum UISounds
        {
            OnBack = 0,
            OnChoose = 1,
            OnElementChosen = 2,
            OnWin = 3,
            OnLose = 4
        }

        public UISounds _uiSounds;
        public AudioClip AudioClip;
    }
}