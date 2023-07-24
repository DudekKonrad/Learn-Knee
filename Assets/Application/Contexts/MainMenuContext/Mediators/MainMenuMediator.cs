using UnityEngine;

namespace Application.MainMenuContext.Mediators
{
    public class MainMenuMediator : MonoBehaviour
    {
        public void OnExitButtonClick()
        {
            UnityEngine.Application.Quit();
        }
    }
}
