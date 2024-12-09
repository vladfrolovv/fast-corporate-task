using Menu.ColorPicker;
using UnityEngine;
using Zenject;
namespace Installers
{
    public class MenuSceneInstaller : MonoInstaller
    {

        [SerializeField] private UnityEngine.Camera _mainCamera;
        [SerializeField] private Canvas _mainCanvas;

        public override void InstallBindings()
        {
            Container.Bind<ColorsPanelParent>().FromComponentInHierarchy().AsSingle();
            Container.BindInterfacesAndSelfTo<ColorsPanel>().AsSingle().NonLazy();

            Container.Bind<UnityEngine.Camera>().FromInstance(_mainCamera).AsSingle();
            Container.Bind<Canvas>().FromInstance(_mainCanvas).AsSingle();
        }

    }
}
