using UnityEngine;
using UnityEngine.UI;

namespace Application.MainMenuContext.Views
{
    public class RankingRecordView : MonoBehaviour
    {
        [SerializeField] private Text _positionText;
        [SerializeField] private Text _nameText;
        [SerializeField] private Text _scoreText;

        public void SetPosition(int position) => _positionText.text = position.ToString();
        public void SetName(string id) => _nameText.text = id;
        public void SetScore(int score) => _scoreText.text = score.ToString();
    }
}