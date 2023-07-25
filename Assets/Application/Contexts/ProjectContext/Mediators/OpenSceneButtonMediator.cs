using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Application.ProjectContext.Mediators
{
    [RequireComponent(typeof(Button))]
    public class OpenSceneButtonMediator : MonoBehaviour
    {
        [SerializeField] private string _sceneName;
    
        private Button _button;

        private void Start()
        {
            _button = GetComponent<Button>();
            _button.onClick.AddListener(OnClick);
        }

        private void OnClick()
        {
            SceneManager.LoadScene(_sceneName);
        }
    }
}
