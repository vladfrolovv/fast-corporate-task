using Inputs;
using Players;
using Zenject;
namespace Installers
{
    public class GameplaySceneInstaller : MonoInstaller
    {

        public override void InstallBindings()
        {
            Container.Bind<PlayerView>().FromComponentInHierarchy().AsSingle();
            Container.Bind<PlayerAnimationController>().FromComponentInHierarchy().AsSingle();
            Container.BindInterfacesAndSelfTo<PlayerPresenter>().AsSingle().NonLazy();

            Container.Bind<BaseInputProvider>().FromComponentInHierarchy().AsSingle();
        }

    }
}
