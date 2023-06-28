using UnityEngine;
using UnityEngine.SceneManagement;

namespace Application.MainMenuContext.Mediators
{
    public class MainMenuMediator : MonoBehaviour
    {
        public void OnPlayButtonClick()
        {
            SceneManager.LoadScene("Gameplay");
        
        }

        public void OnQuizButtonClick()
        {
            SceneManager.LoadScene("Quiz");
        }
        public void OnExitButtonClick()
        {
            UnityEngine.Application.Quit();
        }
    }
}
