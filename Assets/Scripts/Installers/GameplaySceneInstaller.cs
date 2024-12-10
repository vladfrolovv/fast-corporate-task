using Gameplay;
using Hazards;
using Hazards.AcidDrops;
using Hazards.Particles;
using Hazards.Puddles;
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
        [SerializeField] private AcidParticles _acidParticlesPrefab;
        [SerializeField] private Shadow _shadowPrefab;
        [SerializeField] private Puddle _puddlePrefab;

        public override void InstallBindings()
        {
            Container.Bind<PlayerView>().FromComponentInHierarchy().AsSingle();
            Container.Bind<PlayerAnimationController>().FromComponentInHierarchy().AsSingle();
            Container.BindInterfacesAndSelfTo<PlayerPresenter>().AsSingle().NonLazy();

            Container.Bind<AcidCloudZone>().FromComponentInHierarchy().AsSingle();
            Container.BindInterfacesAndSelfTo<AcidCloud>().AsSingle().NonLazy();

            Container.Bind<Countdown>().FromComponentInHierarchy().AsSingle();

            Container.Bind<BaseInputProvider>().FromComponentInHierarchy().AsSingle();

            InstallPrefabs();
        }

        private void InstallPrefabs()
        {
            Container.BindFactory<AcidDropInfo, AcidDrop, AcidDropsFactory>()
                .FromPoolableMemoryPool<AcidDropInfo, AcidDrop, AcidDropsPool>(x =>
                    x.WithInitialSize(64).FromComponentInNewPrefab(_acidDropPrefab));

            Container.BindFactory<ShadowInfo, Shadow, ShadowsFactory>()
                .FromPoolableMemoryPool<ShadowInfo, Shadow, TargetsPool>(x =>
                    x.WithInitialSize(32).FromComponentInNewPrefab(_shadowPrefab));

            Container.BindFactory<AcidParticlesInfo, AcidParticles, AcidParticlesFactory>()
                .FromPoolableMemoryPool<AcidParticlesInfo, AcidParticles, AcidParticlesPool>(x =>
                    x.WithInitialSize(64).FromComponentInNewPrefab(_acidParticlesPrefab));

            Container.BindFactory<PuddleInfo, Puddle, PuddlesFactory>()
                .FromPoolableMemoryPool<PuddleInfo, Puddle, PuddlesPool>(x =>
                    x.WithInitialSize(16).FromComponentInNewPrefab(_puddlePrefab));
        }

        private class AcidDropsPool : MonoPoolableMemoryPool<AcidDropInfo, IMemoryPool, AcidDrop>
        {
        }

        private class TargetsPool : MonoPoolableMemoryPool<ShadowInfo, IMemoryPool, Shadow>
        {
        }

        private class AcidParticlesPool : MonoPoolableMemoryPool<AcidParticlesInfo, IMemoryPool, AcidParticles>
        {
        }

        private class PuddlesPool : MonoPoolableMemoryPool<PuddleInfo, IMemoryPool, Puddle>
        {
        }

    }
}
