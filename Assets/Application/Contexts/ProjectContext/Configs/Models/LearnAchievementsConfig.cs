using Application.ProjectContext.Achievements;
using UnityEngine;

namespace Application.ProjectContext.Configs.Models
{
    [CreateAssetMenu(fileName = "LearnAchievementsConfig", menuName = "Configs/LearnAchievementsConfig", order = 0)]
    public class LearnAchievementsConfig : AppConfigModel, IAchievementsConfig
    {
        [SerializeField] private LearnAchievement[] _achievementsArray = { };
        public IAchievement[] Achievements => _achievementsArray;
    }
}