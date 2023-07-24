using Application.ProjectContext.Signals;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

[RequireComponent(typeof(Text))]
public class ElementTitleView : MonoBehaviour
{
    [Inject] private readonly SignalBus _signalBus;
    private Text _text;

    [Inject]
    private void Construct()
    {
        _signalBus.Subscribe<LearnProjectSignals.ElementChosenSignal>(OnElementChosenSignal);
        _text = GetComponent<Text>();
    }

    private void OnElementChosenSignal(LearnProjectSignals.ElementChosenSignal signal)
    {
        _text.text = $"{signal.Element.name}";
    }
}
