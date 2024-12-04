using System;
using Players;
using UniRx;
using UnityEngine;
using Zenject;
namespace Hazards
{
    public class AcidDrop : MonoBehaviour, IPoolable<AcidDropInfo, IMemoryPool>, IDisposable
    {

        private AcidDropInfo _info;
        private IMemoryPool _pool;

        private CompositeDisposable _compositeDisposable = new();

        [SerializeField] private Collider _collider;

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
                Position += Vector3.down * 9.8f * Time.deltaTime;
            }).AddTo(_compositeDisposable);
        }

        public void OnDespawned()
        {
        }

        public void Dispose()
        {
            _pool?.Despawn(this);

            _pool = null;
            _info = null;
            gameObject.SetActive(false);

            _compositeDisposable?.Dispose();
        }

    }
}
