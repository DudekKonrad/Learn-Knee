using Application.GameplayContext.Models;
using Application.ProjectContext;
using Zenject;

namespace Application.GameplayContext
{
    public class GameplayContextInstaller : MonoInstaller<GameplayContextInstaller>
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<PlayerInputModel>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<SelectedElementService>().AsSingle().NonLazy();
        }
    }
}
