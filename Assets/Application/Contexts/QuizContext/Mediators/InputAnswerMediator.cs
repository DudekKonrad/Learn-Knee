using Application.ProjectContext.Configs;
using Application.ProjectContext.Signals;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Application.QuizContext.Mediators
{
    [RequireComponent(typeof(InputField))]
    public class InputAnswerMediator : MonoBehaviour
    {
        [Inject] private readonly SignalBus _signalBus;
        [Inject] private readonly LearnGameConfig _gameConfig;
        
        [SerializeField] private InputField _input;
        private Image _image;
        private Color _imageStartingColor;
        
        private void Start()
        {
            _input = GetComponent<InputField>();
            _input.onSubmit.AddListener(OnSubmit);
            _image = GetComponent<Image>();
            _imageStartingColor = _image.color;
            
            _signalBus.Subscribe<LearnProjectSignals.GameFinished>(OnGameFinished);
        }

        private void OnGameFinished(LearnProjectSignals.GameFinished obj)
        {
            _input.enabled = false;
        }

        private void OnSubmit(string answer)
        {
            _signalBus.Fire(new LearnProjectSignals.AnswerGivenSignal(_input.text));
        }

        public void GoodAnswer()
        {
            _image.DOColor(Color.green, _gameConfig.ChooseColorDuration);
        }

        public void BadAnswer()
        {
            _image.DOColor(Color.red, _gameConfig.ChooseColorDuration);
        }

        public void SetDefault()
        {
            _input.text = string.Empty;
            _image.DOColor(_imageStartingColor, _gameConfig.ChooseColorDuration);
        }
    }
}
