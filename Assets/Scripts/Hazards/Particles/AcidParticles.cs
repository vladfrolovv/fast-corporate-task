using System;
using UniRx;
using UnityEngine;
using Zenject;
namespace Hazards.Particles
{
    public class AcidParticles : MonoBehaviour, IPoolable<AcidParticlesInfo, IMemoryPool>, IDisposable
    {

        private IMemoryPool _pool;
        private AcidParticlesInfo _info;

        public void OnSpawned(AcidParticlesInfo info, IMemoryPool pool)
        {
            _info = info;
            _pool = pool;

            Observable.Timer(TimeSpan.FromSeconds(2f)).Subscribe(delegate
            {
                _pool.Despawn(this);
            });
        }

        public void OnDespawned()
        {
            _pool = null;
        }

        public void Dispose()
        {
        }
        
    }
}
