﻿using Application.ProjectContext.Signals;
using Application.Utils.SoundService;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Zenject;

namespace Application.ProjectContext.Mediators
{
    [RequireComponent(typeof(Button))]
    public class OpenSceneButtonMediator : MonoBehaviour
    {
        [Inject] private readonly SignalBus _signalBus;
        
        [SerializeField] private string _sceneName;
    
        private Button _button;

        private void Start()
        {
            _button = GetComponent<Button>();
            _button.onClick.AddListener(OnClick);
        }

        private void OnClick()
        {
            DOVirtual.DelayedCall(2f, () => SceneManager.LoadScene(_sceneName));
            _signalBus.Fire(new LearnProjectSignals.PlaySoundSignal(AudioClipModel.UISounds.OnChoose));
        }
    }
}