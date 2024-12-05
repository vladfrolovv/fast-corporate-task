using Hazards;
using Hazards.AcidDrops;
using Hazards.Targets;
using Inputs;
using Players;
using UnityEngine;
using Zenject;
namespace Installers
{
    public class GameplaySceneInstaller : MonoInstaller
    {

        [SerializeField] private AcidDrop _acidDropPrefab;
        [SerializeField] private Target _targetPrefab;

        public override void InstallBindings()
        {
            Container.Bind<PlayerView>().FromComponentInHierarchy().AsSingle();
            Container.Bind<PlayerAnimationController>().FromComponentInHierarchy().AsSingle();
            Container.BindInterfacesAndSelfTo<PlayerPresenter>().AsSingle().NonLazy();

            Container.Bind<AcidZone>().FromComponentInHierarchy().AsSingle();
            Container.BindInterfacesAndSelfTo<AcidCloud>().AsSingle().NonLazy();

            Container.Bind<BaseInputProvider>().FromComponentInHierarchy().AsSingle();

            InstallPrefabs();
        }

        private void InstallPrefabs()
        {
            Container.BindFactory<AcidDropInfo, AcidDrop, AcidDropsFactory>()
                .FromPoolableMemoryPool<AcidDropInfo, AcidDrop, AcidDropsPool>(x =>
                    x.WithInitialSize(32).FromComponentInNewPrefab(_acidDropPrefab));

            Container.BindFactory<TargetInfo, Target, TargetsFactory>()
                .FromPoolableMemoryPool<TargetInfo, Target, TargetsPool>(x =>
                    x.WithInitialSize(32).FromComponentInNewPrefab(_targetPrefab));
        }

        private class AcidDropsPool : MonoPoolableMemoryPool<AcidDropInfo, IMemoryPool, AcidDrop>
        {
        }

        private class TargetsPool : MonoPoolableMemoryPool<TargetInfo, IMemoryPool, Target>
        {
        }

    }
}
