using System;
using DataProxies;
using UniRx;
using UnityEngine;
using Zenject;
namespace Hazards.AcidDrops
{
    public class AcidDrop : MonoBehaviour, IPoolable<AcidDropInfo, IMemoryPool>, IDisposable
    {

        [SerializeField] private Collider _collider;

        private AcidDropInfo _info;
        private IMemoryPool _pool;

        private IDisposable _fixedUpdateDisposable;

        public event Action OnAcidDropDispose;

        public void OnSpawned(AcidDropInfo info, IMemoryPool pool)
        {
            _info = info;
            _pool = pool;

            Drop();
        }

        private void Drop()
        {
            _fixedUpdateDisposable = Observable
                .EveryFixedUpdate()
                .Subscribe(delegate
                {
                    float verticalPull = GlobalDataProxy.GRAVITY * Time.deltaTime * _info.SpeedMultiplier;
                    transform.position += Vector3.down * verticalPull;
                }).AddTo(this);
        }

        public void OnHit()
        {
            Dispose();
        }

        public void OnDespawned()
        {
            _fixedUpdateDisposable?.Dispose();
            _pool = null;
        }

        public void Dispose()
        {
            OnAcidDropDispose?.Invoke();

            _pool?.Despawn(this);
        }

    }
}
