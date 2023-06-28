using Application.GameplayContext.Models;
using Zenject;

namespace Application.GameplayContext
{
    public class GameplayContextInstaller : MonoInstaller<GameplayContextInstaller>
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<PlayerInputModel>().AsSingle();
        }
    }
}
