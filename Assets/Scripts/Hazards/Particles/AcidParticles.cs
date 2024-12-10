using System;
using UniRx;
using UnityEngine;
using Utils.Containers;
using Zenject;
namespace Hazards.Particles
{
    public class AcidParticles : MonoBehaviour, IPoolable<AcidParticlesInfo, IMemoryPool>, IDisposable
    {

        [SerializeField] private ParticlesMap _particlesMap;

        private AcidParticlesInfo _info;
        private IMemoryPool _pool;

        public void OnSpawned(AcidParticlesInfo info, IMemoryPool pool)
        {
            _info = info;
            _pool = pool;

            _particlesMap[ParticlesType.Acid].SetActive(info.Type == ParticlesType.Acid);
            _particlesMap[ParticlesType.Blood].SetActive(info.Type == ParticlesType.Blood);

            Observable.Timer(TimeSpan.FromSeconds(1f)).Subscribe(delegate
            {
                _pool?.Despawn(this);
            }).AddTo(this);
        }

        public void OnDespawned()
        {
            _pool = null;
        }

        public void Dispose()
        {
            _pool?.Despawn(this);
        }

        [Serializable]
        public class ParticlesMap : KeyValueList<ParticlesType, GameObject>
        {
        }

    }
}
