using Application.ProjectContext.Configs;
using Application.ProjectContext.Signals;
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
            SignalBusInstaller.Install(Container);
            Container.DeclareSignal<LearnProjectSignals.ElementChosenSignal>();
        }
    }
}