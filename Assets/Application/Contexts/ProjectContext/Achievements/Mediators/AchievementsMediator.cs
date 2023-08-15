using System.Collections.Generic;
using Application.ProjectContext.Achievements.Services;
using Application.ProjectContext.Achievements.Views;
using UnityEngine;
using Zenject;

namespace Application.ProjectContext.Achievements.Mediators
{
    public class AchievementsMediator : MonoBehaviour
    {
        [Inject] private readonly DiContainer _diContainer;
        [Inject] private readonly IAchievementService _achievementService;
        [Inject] private readonly IAchievementsConfig _achievementsConfig;
        [SerializeField] private GameObject _content;
        [SerializeField] private GameObject _achievementPrefab;
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
                var achievementProcessor = _achievementService.GetProgress(achievement);
                var achievementView = achievementPrefab.GetComponent<AchievementView>();
                achievementView.SetTitle(achievement.Title);
                achievementView.ProgressVisible = achievementProcessor.Item4;
                achievementView.SetProgress(achievementProcessor.Item2, achievementProcessor.Item3);
                _achievementsPrefabs.Add(achievement, achievementPrefab);
                Debug.Log($"Created Achievement: {achievement.Type}");
            }
        }

        private void ActualizeProgress()
        {
            if (_achievementsPrefabs.Count > 0)
            {
                foreach (var achievement in _achievementsPrefabs)
                {
                    var achievementProcessor = _achievementService.GetProgress(achievement.Key);
                    var achievementView = achievement.Value.GetComponent<AchievementView>();
                    achievementView.ProgressVisible = achievementProcessor.Item4;
                    achievementView.SetProgress(achievementProcessor.Item2, achievementProcessor.Item3);
                }
            }
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