using Application.ProjectContext.Configs;
using Application.ProjectContext.Signals;
using UnityEngine;
using Zenject;

namespace Application.ProjectContext
{
    public class ProjectContextInstaller : MonoInstaller<ProjectContextInstaller>
    {
        [SerializeField] private LearnGameConfig _gameConfig;
        [SerializeField] private LearnModel _learnModel;
        public override void InstallBindings()
        {
            Container.BindInstance(_gameConfig);
            Container.BindInstance(_learnModel);
            SignalBusInstaller.Install(Container);
            Container.DeclareSignal<LearnProjectSignals.ElementChosenSignal>();
        }
    }
}