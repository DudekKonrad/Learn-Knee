using Application.ProjectContext.Configs;
using Application.ProjectContext.Configs.Models;
using Application.ProjectContext.Services;
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
            BindConfigs();
            SignalBusInstaller.Install(Container);
            //Container.BindInterfacesAndSelfTo<SoundService>().AsSingle().NonLazy();
            Container.DeclareSignal<LearnProjectSignals.ElementChosenSignal>();
            Container.DeclareSignal<LearnProjectSignals.UINavigationSignal>();
            Container.DeclareSignal<LearnProjectSignals.AnswerGivenSignal>();
            Container.DeclareSignal<LearnProjectSignals.TimeIsUpSignal>();
            Container.DeclareSignal<LearnProjectSignals.GameFinished>();
            Container.DeclareSignal<LearnProjectSignals.PlaySoundSignal>();
            Container.DeclareSignal<LearnProjectSignals.StopSoundSignal>();
        }
        
        private void BindConfigs()
        {
            var configs = Resources.LoadAll<AppConfigModel>("Configs");

            foreach (var config in configs)
            {
                Container.BindInterfacesAndSelfTo(config.GetType()).FromInstance(config).AsSingle().NonLazy();
            }
        }
    }
}