using Application.ProjectContext.Configs;
using Application.ProjectContext.Services;
using Application.ProjectContext.Signals;
using Application.Utils;
using Application.Utils.SoundService;
using DG.Tweening;
using UnityEngine;
using Zenject;

namespace Application.GameplayContext
{
    public enum ElementType{
        Tibia,
        Fibula,
        Femur,
        Patella,
        MedialCondyle,
        LateralMeniscus,
        MedialMeniscus,
        TibialJoint,
        PatellaJoint,
        SagittalJoint,
        TransverseLigament,
        AnteriorCruciateLigament,
        PosteriorCruciateLigament,
        PosteriorMeniscusFemoralLigament,
        MedialCollateralLigament,
        LateralCollatteralLigament,
        PattelarLigament
    }
    
    [RequireComponent(typeof(Renderer))]
    public class ModelElementView : MonoBehaviour, ISelectionResponse
    {
        [Inject] private readonly SignalBus _signalBus;
        [Inject] private readonly LearnGameConfig _gameConfig;

        [SerializeField] private ElementType _elementType;
        [SerializeField] private ModelElementView[] _neighbours;
        [SerializeField] private bool _allNeighbours;
        [SerializeField] private Vector3 _exposeRotation;
        [SerializeField] private string _translationKey;
        
        private Renderer _renderer;
        public bool IsSelected { get; set; }
        public bool IsViewed { set; get; }
        private Vector3 _startingPosition;
        private Color _staringColor;
        public string Name => GameObject.name;
        public ElementType ElementType => _elementType;
        public ModelElementView[] Neighbours => _neighbours;
        public bool AllNeighbour => _allNeighbours;
        public Vector3 ExposeRotation => _exposeRotation;
        public string TranslationKey => _translationKey;

        [Inject]
        private void Construct()
        {
            _signalBus.Subscribe<LearnProjectSignals.ElementChosenSignal>(OnElementChosen);
        }

        private void OnElementChosen(LearnProjectSignals.ElementChosenSignal obj)
        {
            if (obj.Element != this)
            {
                _renderer.material.DOColor(_staringColor, _gameConfig.ChooseColorDuration);
                IsViewed = false;
            }
        }

        private void Start()
        {
            _startingPosition = transform.localPosition;
            _renderer = GetComponent<Renderer>();
            _staringColor = _renderer.material.color;
        }

        public void Disappear()
        {
            _renderer.material.DOColor(new Color(_renderer.material.color.r, _renderer.material.color.g, _renderer.material.color.b, 0), 0.3f).OnComplete(
                () =>
                {
                    gameObject.SetActive(false);
                });
        }
        public void Appear()
        {
            gameObject.SetActive(true);
            _renderer.material.DOColor(new Color(_renderer.material.color.r, _renderer.material.color.g, _renderer.material.color.b, 1f), 0.3f);
        }

        public void Expose()
        {
            GameObject.Find("Knee_Center_Model").transform.DORotate(_exposeRotation, _gameConfig.ExposeTime);
        }

        public GameObject GameObject => gameObject;

        public void OnSelect()
        {
            IsSelected = true;
        }

        public void OnDeselect()
        {
            IsSelected = false;
        }

        public void OnChosen()
        {
            if (!IsViewed)
            {
                _renderer.material.DOColor(_gameConfig.ChooseColor, _gameConfig.ChooseColorDuration);
                _signalBus.Fire(new LearnProjectSignals.PlaySoundSignal(AudioClipModel.UISounds.OnElementChosen));
                IsViewed = true;
                _signalBus.Fire(new LearnProjectSignals.ElementChosenSignal(this));
            }
            else
            {
                _renderer.material.DOColor(_staringColor, _gameConfig.ChooseColorDuration);
                _signalBus.Fire(new LearnProjectSignals.ElementUnChosenSignal(this));
                _signalBus.Fire(new LearnProjectSignals.PlaySoundSignal(AudioClipModel.UISounds.OnElementChosen));
                IsViewed = false;
            }
        }
    }
}