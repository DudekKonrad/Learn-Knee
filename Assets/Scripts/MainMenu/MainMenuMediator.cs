using UnityEngine;
using UnityEngine.SceneManagement;

namespace MainMenu
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
            Application.Quit();
        }
    }
}
