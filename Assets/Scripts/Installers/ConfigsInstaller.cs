using Hazards;
using Menu.ColorPicker;
using Players;
using UnityEngine;
using Zenject;
namespace Installers
{
    [CreateAssetMenu(fileName = "ConfigsInstaller", menuName = "SO/ConfigsInstaller" )]
    public class ConfigsInstaller : ScriptableObjectInstaller
    {

        [SerializeField] private PlayerConfig _playerConfig;
        [SerializeField] private AcidCloudConfig _acidCloudConfig;
        [SerializeField] private ColorsConfig _colorsConfig;
        [SerializeField] private PuddlesConfig _puddlesConfig;

        public override void InstallBindings()
        {
            Container.BindInstance(_playerConfig);
            Container.BindInstance(_acidCloudConfig);
            Container.BindInstance(_colorsConfig);
            Container.BindInstance(_puddlesConfig);
        }
        
    }
}
