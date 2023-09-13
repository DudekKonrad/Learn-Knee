using System.Collections.Generic;
using System.Linq;
using Application.ProjectContext.Achievements.Views;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Application.ProjectContext.Achievements.Mediators
{
    public class AchievementsMediator : MonoBehaviour
    {
        [Inject] private readonly DiContainer _diContainer;
        [Inject] private readonly IAchievementsConfig _achievementsConfig;
        
        [SerializeField] private GameObject _content;
        [SerializeField] private GameObject _achievementPrefab;
        [SerializeField] private Text _counter;
        
        private Dictionary<IAchievement, GameObject> _achievementsPrefabs = new Dictionary<IAchievement, GameObject>();

        private void Start()
        {
            if (_achievementsPrefabs.Count == 0)
            {
                Initialize();
            }
        }

        private void Initialize()
        {
            foreach (var achievement in _achievementsConfig.Achievements)
            {
                var achievementPrefab = _diContainer.InstantiatePrefab(_achievementPrefab, _content.transform);
                var achievementView = achievementPrefab.GetComponent<AchievementView>();
                achievementView.LocalizedText.SetTranslationKey(achievement.TranslationKey);
                achievementView.SetProgress(achievement.Progress, 
                    achievement.Threshold, achievement.ProgressNormalized, achievement.IsCompleted, achievement.IsProgressVisible);
                _achievementsPrefabs.Add(achievement, achievementPrefab);
            }
        }

        private void ActualizeProgress()
        {
            if (_achievementsPrefabs.Count > 0)
            {
                foreach (var achievement in _achievementsPrefabs)
                {
                    var achievementView = achievement.Value.GetComponent<AchievementView>();
                    achievementView.SetProgress(achievement.Key.Progress, 
                        achievement.Key.Threshold, achievement.Key.ProgressNormalized, achievement.Key.IsCompleted, achievement.Key.IsProgressVisible);
                }
            }
            _counter.text = $"{_achievementsConfig.Achievements.Count(_ => _.IsCompleted)}/{_achievementsConfig.Achievements.Length}";
        }
        
        private void OnEnable()
        {
            if (_achievementsPrefabs.Count == 0)
            {
                Initialize();
            }

            ActualizeProgress();
        }
    }
}