using Application.GameplayContext.Models;
using Zenject;

namespace Application.MainMenuContext
{
    public class MainMenuInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<PlayerInputModel>().AsSingle();
        }
    }
}