using Application.GameplayContext.Models;
using Application.ProjectContext;
using Application.ProjectContext.Signals;
using Zenject;

namespace Application.GameplayContext
{
    public class GameplayContextInstaller : MonoInstaller<GameplayContextInstaller>
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<PlayerInputModel>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<SelectedElementService>().AsSingle().NonLazy();
            Container.DeclareSignal<LearnProjectSignals.LoadSceneSignal>();
        }
    }
}
