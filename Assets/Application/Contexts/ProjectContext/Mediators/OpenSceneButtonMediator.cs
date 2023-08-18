using Application.ProjectContext.Signals;
using Application.Utils.SoundService;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Application.ProjectContext.Mediators
{
    [RequireComponent(typeof(Button))]
    public class OpenSceneButtonMediator : MonoBehaviour
    {
        [Inject] private readonly SignalBus _signalBus;
        [Inject] private readonly ZenjectSceneLoader _sceneLoader;

        
        [SerializeField] private string _sceneName;
    
        private Button _button;

        private void Start()
        {
            _button = GetComponent<Button>();
            _button.onClick.AddListener(OnClick);
        }

        private void OnClick()
        {
            _signalBus.Fire(new LearnProjectSignals.ShowLoadingScreenSignal());
            var asyncOp = _sceneLoader.LoadSceneAsync(_sceneName);
            _signalBus.Fire(new LearnProjectSignals.PlaySoundSignal(AudioClipModel.UISounds.OnChoose));
        }
    }
}