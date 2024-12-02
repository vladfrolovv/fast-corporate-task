using Transitions;
using Zenject;
namespace Installers
{
    public class ProjectContextInstaller : MonoInstaller
    {

        public override void InstallBindings()
        {
            Container.Bind<Transition>().FromComponentInHierarchy().AsSingle();
        }

    }
}
