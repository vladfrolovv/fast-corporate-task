using System;
using UnityEngine;
using Zenject;
namespace Hazards.Targets
{
    public class Target  : MonoBehaviour, IPoolable<TargetInfo, IMemoryPool>, IDisposable
    {

        private TargetInfo _info;
        private IMemoryPool _pool;

        public void OnSpawned(TargetInfo info, IMemoryPool pool)
        {
            _info = info;
            _pool = pool;
        }

        public void OnDespawned()
        {
            _pool = null;
        }

        public void Dispose()
        {
            _pool?.Despawn(this);
        }

    }
}
