using System;
using UniRx;
using UnityEngine;
using Zenject;
namespace Hazards
{
    public class AcidDrop : MonoBehaviour, IPoolable<AcidDropInfo, IMemoryPool>, IDisposable
    {

        private AcidDropInfo _info;
        private IMemoryPool _pool;

        public Vector3 Position
        {
            get => transform.position;
            set => transform.position = value;
        }

        public void OnSpawned(AcidDropInfo info, IMemoryPool pool)
        {
            _info = info;
            _pool = pool;

            Observable.EveryUpdate().Subscribe(delegate
            {
            });
        }

        public void OnDespawned()
        {
        }

        public void Dispose()
        {
        }

    }
}
