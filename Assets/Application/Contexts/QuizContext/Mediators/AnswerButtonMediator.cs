using Application.ProjectContext.Signals;
using Application.Utils;
using Application.Utils.SoundService;
using UnityEngine;
using UnityEngine.UI;
using Zenject;
using ElementType = Application.GameplayContext.ElementType;

namespace Application.QuizContext.Mediators
{
    [RequireComponent(typeof(Button))]
    public class AnswerButtonMediator : MonoBehaviour
    {
        [Inject] private readonly SignalBus _signalBus;
        [SerializeField] private Text _text;
        [SerializeField] private LocalizedText _localizedText;
        private Color _startingColor;
        private Animator _animator;

        public LocalizedText LocalizedText => _localizedText;
        public Button Button;
        public ElementType ButtonElementType;

        private void Start()
        {
            _animator = GetComponent<Animator>();
            Button.onClick.AddListener(OnClick);
            _startingColor = _text.color;
            _signalBus.Subscribe<LearnProjectSignals.GameFinished>(OnGameFinished);
        }

        private void OnGameFinished(LearnProjectSignals.GameFinished signal)
        {
            Button.enabled = false;
        }

        private void OnClick()
        {
            _signalBus.Fire(new LearnProjectSignals.PlaySoundSignal(AudioClipModel.UISounds.OnChoose));
            _signalBus.Fire(new LearnProjectSignals.AnswerGivenSignal(ButtonElementType, this));
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
