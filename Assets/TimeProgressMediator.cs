using System;
using Application.ProjectContext.Signals;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class TimeProgressMediator : MonoBehaviour
{
    [Inject] private readonly SignalBus _signalBus;

    [SerializeField] private Image _image;
    [SerializeField] private Text _timeLeftText;
    public float Time;
    public bool CanTime = true;
    public float _startingTime;
    private float _remainingTimePercent => Time / _startingTime;
    
    private void Start()
    {
        _startingTime = Time;
    }
    
    private void Update()
    {
        if (Time > 0 && CanTime)
        {
            Time -= UnityEngine.Time.deltaTime;
            _image.fillAmount = 1f - _remainingTimePercent;
            var timeSpan = TimeSpan.FromSeconds(Time);
            var timeFormatted = timeSpan.ToString(@"mm\:ss");
            _timeLeftText.text = timeFormatted;
        }
        else if (CanTime)
        {
            _signalBus.Fire(new LearnProjectSignals.TimeIsUpSignal());
        }
    }
}