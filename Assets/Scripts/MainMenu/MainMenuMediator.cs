using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuMediator : MonoBehaviour
{
    public void OnPlayButtonClick()
    {
        SceneManager.LoadScene("Gameplay");
        
    }
    public void OnExitButtonClick()
    {
        Application.Quit();
    }
}
