using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Application.MainMenuContext.Views
{
    public class RankingRecordView : MonoBehaviour
    {
        [SerializeField] private Text _positionText;
        [SerializeField] private Text _nameText;
        [SerializeField] private Text _scoreText;
        [SerializeField] private Image _image;
        [SerializeField] private Color _goldenColor;
        [SerializeField] private Color _silverColor;
        [SerializeField] private Color _bronzeColor;
        [SerializeField] private float _fadeDuration;
        
        private int _position;
        
        private void RefreshView()
        {
            switch (_position)  
            {
                case 1:
                    _image.DOColor(_goldenColor, _fadeDuration);
                    break;
                case 2:
                    _image.DOColor(_silverColor, _fadeDuration);
                    break;
                case 3:
                    _image.DOColor(_bronzeColor, _fadeDuration);
                    break;
            }
        }

        public void SetPosition(int position)
        {
            _position = position;
            _positionText.text = _position.ToString();
            RefreshView();
        }

        public void SetName(string id) => _nameText.text = id;
        public void SetScore(int score) => _scoreText.text = score.ToString();
    }
}