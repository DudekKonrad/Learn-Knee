using Application.ProjectContext.Signals;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Application.QuizContext.Mediators
{
    [RequireComponent(typeof(Button))]
    public class AnswerButtonMediator : MonoBehaviour
    {
        [Inject] private readonly SignalBus _signalBus;
        [SerializeField] private Text _text;
        public Button Button;
        private Color _startingColor;
        private Animator _animator;
        
        private void Start()
        {
            _animator = GetComponent<Animator>();
            Button.onClick.AddListener(OnClick);
            _startingColor = _text.color;
        }

        private void OnClick()
        {
            _signalBus.Fire(new LearnProjectSignals.AnswerGivenSignal(_text.text, this));
        }

        public void SetText(string text)
        {
            _text.text = $"{text}";
        }
        public void GoodAnswer()
        {
            _animator.SetTrigger("GoodAnswer");
        }

        public void BadAnswer()
        {
            _animator.SetTrigger("BadAnswer");
        }
        public void SetTextDefaultColor()
        {
            _text.color = _startingColor;
        }
    }
}
