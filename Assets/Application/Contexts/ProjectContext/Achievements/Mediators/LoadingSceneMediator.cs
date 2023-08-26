using System.Collections;
using Application.ProjectContext.Signals;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace Application.ProjectContext.Achievements.Mediators
{
    public class LoadingSceneMediator : MonoBehaviour
    {
        [Inject] private readonly SignalBus _signalBus;

        [SerializeField] private GameObject _loadingScreen;
        
        private void Start()
        {
            _signalBus.Subscribe<LearnProjectSignals.LoadSceneSignal>(OnLoadSceneSignal);
        }

        private void OnLoadSceneSignal(LearnProjectSignals.LoadSceneSignal signal)
        {
            StartCoroutine(LoadScene(signal.SceneName));
        }

        private IEnumerator LoadScene(string sceneName)
        {
            _loadingScreen.SetActive(true);
            var operation = SceneManager.LoadSceneAsync(sceneName);
            while (!operation.isDone)
            {
                yield return null;
            }
            yield return new WaitForSeconds(2.0f);
        }
    }
}