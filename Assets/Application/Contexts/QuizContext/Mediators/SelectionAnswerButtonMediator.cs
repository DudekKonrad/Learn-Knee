using Application.GameplayContext;
using Application.ProjectContext.Signals;
using Application.Utils.SoundService;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Application.QuizContext.Mediators
{
    [RequireComponent(typeof(Button))]
    public class SelectionAnswerButtonMediator : MonoBehaviour
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
            _signalBus.Fire(new LearnProjectSignals.PlaySoundSignal(AudioClipModel.UISounds.OnChoose));
            _signalBus.Fire(new LearnProjectSignals.AnswerGivenSignal());
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
