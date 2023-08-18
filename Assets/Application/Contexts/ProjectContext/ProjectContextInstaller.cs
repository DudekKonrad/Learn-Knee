using Application.ProjectContext.Achievements.Services;
using Application.ProjectContext.Configs;
using Application.ProjectContext.Configs.Models;
using Application.ProjectContext.Models;
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
            Container.BindInterfacesAndSelfTo<SoundService>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<AchievementsService>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<DescriptionsModel>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<TranslationsService>().AsSingle().NonLazy();
            DeclareSignals();
        }
        
        private void BindConfigs()
        {
            var configs = Resources.LoadAll<AppConfigModel>("Configs");

            foreach (var config in configs)
            {
                Container.BindInterfacesAndSelfTo(config.GetType()).FromInstance(config).AsSingle().NonLazy();
            }
        }

        private void DeclareSignals()
        {
            Container.DeclareSignal<LearnProjectSignals.ElementChosenSignal>();
            Container.DeclareSignal<LearnProjectSignals.ElementUnChosenSignal>();
            Container.DeclareSignal<LearnProjectSignals.UINavigationSignal>();
            Container.DeclareSignal<LearnProjectSignals.AnswerGivenSignal>();
            Container.DeclareSignal<LearnProjectSignals.GameFinished>();
            Container.DeclareSignal<LearnProjectSignals.PlaySoundSignal>();
            Container.DeclareSignal<LearnProjectSignals.StopSoundSignal>();
            Container.DeclareSignal<LearnProjectSignals.ShowLoadingScreenSignal>();
            Container.DeclareSignal<LearnProjectSignals.HideLoadingScreenSignal>();
            Container.DeclareSignal<LearnProjectSignals.LanguageChangedSignal>();
        }
    }
}