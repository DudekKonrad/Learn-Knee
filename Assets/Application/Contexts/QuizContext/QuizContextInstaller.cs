using Application.ProjectContext.Signals;
using Application.QuizContext.Models;
using Zenject;

namespace Application.QuizContext
{
    public class QuizContextInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<QuizPlayerModel>().AsSingle();
            Container.DeclareSignal<LearnProjectSignals.LoadSceneSignal>();
        }
    }
}