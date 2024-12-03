using Players;
using UnityEngine;
using Zenject;
namespace Installers
{
    [CreateAssetMenu(fileName = "ConfigsInstaller", menuName = "SO/ConfigsInstaller" )]
    public class ConfigsInstaller : ScriptableObjectInstaller
    {

        [SerializeField] private PlayerConfig _playerConfig;

        public override void InstallBindings()
        {
            Container.BindInstance(_playerConfig);
        }
        
    }
}
