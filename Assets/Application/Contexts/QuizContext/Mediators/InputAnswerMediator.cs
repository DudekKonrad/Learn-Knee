using UnityEngine;
using UnityEngine.UI;

namespace Application.QuizContext.Mediators
{
    [RequireComponent(typeof(InputField))]
    public class InputAnswerMediator : MonoBehaviour
    {
        private InputField _input;

        private void Start()
        {
            _input = GetComponent<InputField>();
            _input.onSubmit.AddListener(OnSubmit);
        }

        private void OnSubmit(string answer)
        {
        
        }
    }
}
