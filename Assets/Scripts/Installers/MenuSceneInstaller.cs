using Menu.ColorPicker;
using UnityEngine;
using Zenject;
namespace Installers
{
    public class MenuSceneInstaller : MonoInstaller
    {

        [SerializeField] private DnDColorPanelItem _dnDColorPanelItemPrefab;

        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<ColorsPanel>().AsSingle().NonLazy();
            Container.Bind<ColorsPanelParent>().FromComponentInHierarchy().AsSingle();

            Container.Bind<DnDColorPanelItem>().FromComponentInNewPrefab(_dnDColorPanelItemPrefab).AsSingle();
        }

    }
}
