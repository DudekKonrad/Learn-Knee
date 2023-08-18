using UnityEngine;
using UnityEngine.UI;

namespace Application.ProjectContext.Achievements.Views
{
    public class AchievementView : MonoBehaviour
    {
        [SerializeField] private Image _progress;
        [SerializeField] private Text _progressLabel;
        [SerializeField] private LocalizedText _localizedText;
        [SerializeField] private Image _fillIcon;

        public LocalizedText LocalizedText => _localizedText;

        public virtual void SetProgress(int progress, int threshold, float progressNormalized, bool isCompleted)
        {
            if (_progressLabel != null) _progressLabel.text = $"{progress}/{threshold}";
            if (_progress != null){ _progress.fillAmount = progressNormalized;}
            _fillIcon.gameObject.SetActive(isCompleted);
        }
    }
}