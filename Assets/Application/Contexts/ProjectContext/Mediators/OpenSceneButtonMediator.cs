using Application.ProjectContext.Signals;
using Application.Utils.SoundService;
using Cysharp.Threading.Tasks;
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
        [Inject] private readonly ZenjectSceneLoader _sceneLoader;

        
        [SerializeField] private string _sceneName;
    
        private Button _button;

        private void Start()
        {
            _button = GetComponent<Button>();
            _button.onClick.AddListener(OnClick);
        }

        private async void OnClick()
        {
            _signalBus.Fire(new LearnProjectSignals.ShowLoadingScreenSignal());
            var asyncOp = _sceneLoader.LoadSceneAsync(_sceneName, LoadSceneMode.Single);
            asyncOp.allowSceneActivation = false;
            await UniTask.WhenAll(
                UniTask.WaitUntil(() => asyncOp.progress > 0.8f), UniTask.Delay(600));
            asyncOp.allowSceneActivation = true;
            await UniTask.WaitUntil(() => asyncOp.isDone);
            _signalBus.Fire(new LearnProjectSignals.PlaySoundSignal(AudioClipModel.UISounds.OnChoose));
            DOVirtual.DelayedCall(2f, () => SceneManager.LoadScene(_sceneName));
        }
    }
}