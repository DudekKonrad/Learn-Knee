using Application.GameplayContext.Models;
using Zenject;

namespace Application.QuizContext
{
    public class QuizContextInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<PlayerInputModel>().AsSingle();
        }
    }
}