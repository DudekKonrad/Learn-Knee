using Application.ProjectContext.Configs;
using UnityEngine;
using Zenject;

namespace Application.ProjectContext
{
    public class ProjectContextInstaller : MonoInstaller<ProjectContextInstaller>
    {
        [SerializeField] private LearnGameConfig _gameConfig;
        public override void InstallBindings()
        {
            Container.BindInstance(_gameConfig);
        }
    }
}