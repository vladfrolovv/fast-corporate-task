using UnityEngine;
using Zenject;
namespace Installers
{
    [CreateAssetMenu(fileName = "ConfigsInstaller", menuName = "SO/ConfigsInstaller" )]
    public class ConfigsInstaller : ScriptableObjectInstaller
    {

        public override void InstallBindings()
        {
        }
        
    }
}
