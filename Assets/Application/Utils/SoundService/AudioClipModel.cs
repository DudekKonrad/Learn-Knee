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
            OnRetry = 1,
            OnChoose = 2,
            OnNextOption = 3,
            OnPreviousOption = 4,
            OnNavigationSuccess = 5,
            OnNavigationFailed = 6,
            OnWin = 7,
            OnLose = 8,
            OnScoreCalculating = 9,
            OnAchievementUnlock = 10,
            OnPauseOpen = 11,
            OnPauseClose = 12
        }

        public UISounds _uiSounds;
        public AudioClip AudioClip;
    }
}