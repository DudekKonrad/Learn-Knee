using UnityEngine;
using UnityEngine.UI;

namespace Application.ProjectContext.Achievements.Views
{
    public class AchievementView : MonoBehaviour
    {
        [SerializeField] private Image _achievementIcon;
        //[SerializeField] private Image _achievementPadlock;
        [SerializeField] private Image _progress;
        [SerializeField] private Text _progressLabel;
        [SerializeField] private GameObject _fillView;
        [SerializeField] private Text _title;

        public Image AchievementIcon => _achievementIcon;
        //public Image AchievementPadlock => _achievementPadlock;

        public void SetTitle(string title)
        {
            _title.text = title;
        }

        public bool ProgressVisible
        {
            set
            {
                if (_fillView != null) _fillView.SetActive(value);
            }
        }

        public virtual void SetProgress(string label, float progressNormalized)
        {
            if (_progressLabel != null) _progressLabel.text = label;
            if (_progress != null) _progress.fillAmount = progressNormalized;
        }
    }
}