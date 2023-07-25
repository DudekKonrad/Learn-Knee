using Application.ProjectContext.Signals;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

[RequireComponent(typeof(Button))]
public class AnswerButtonMediator : MonoBehaviour
{
    [Inject] private readonly SignalBus _signalBus;
    [SerializeField] private Text _text;
    public Button Button;
    private Color _startingColor;

    public Text Text => _text;

    private void Start()
    {
        Button = GetComponent<Button>();
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
    public void SetTextColor(Color color)
    {
        _text.color = color;
    }
    public void SetTextDefaultColor()
    {
        _text.color = _startingColor;
    }
}
