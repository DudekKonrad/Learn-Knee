using Application.ProjectContext.Signals;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Application.QuizContext.Mediators
{
    [RequireComponent(typeof(InputField))]
    public class InputAnswerMediator : MonoBehaviour
    {
        [Inject] private readonly SignalBus _signalBus;
        
        private InputField _input;

        private void Start()
        {
            _input = GetComponent<InputField>();
            _input.onSubmit.AddListener(OnSubmit);
        }

        private void OnSubmit(string answer)
        {
            _signalBus.Fire(new LearnProjectSignals.AnswerGivenSignal(_input.text));
        }
    }
}
