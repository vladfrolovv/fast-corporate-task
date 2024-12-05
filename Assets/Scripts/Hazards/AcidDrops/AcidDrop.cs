using System;
using DataProxies;
using DG.Tweening;
using UniRx;
using UnityEngine;
using Zenject;
namespace Hazards.AcidDrops
{
    public class AcidDrop : MonoBehaviour, IPoolable<AcidDropInfo, IMemoryPool>, IDisposable
    {

        [SerializeField] private Collider _collider;
        [SerializeField] private LineRenderer _targetingLine;

        private AcidDropInfo _info;
        private IMemoryPool _pool;

        private IDisposable _fixedUpdateDisposable;

        public void OnSpawned(AcidDropInfo info, IMemoryPool pool)
        {
            _info = info;
            _pool = pool;

            Observable.NextFrame().Subscribe(delegate
            {
                AnimateTargetLine();
            });
        }

        private void AnimateTargetLine()
        {
            _targetingLine.SetPosition(0, transform.position);
            _targetingLine.SetPosition(1, transform.position);

            Sequence sequence = DOTween.Sequence();
            sequence.Append(
                DOTween.To(
                () => _targetingLine.GetPosition(1),
                t => _targetingLine.SetPosition(1, t), _info.DestinationPoint, .32f));

            sequence.AppendCallback(Drop);
            sequence.Play();
        }

        private void Drop()
        {
            _fixedUpdateDisposable = Observable
                .EveryFixedUpdate()
                .Subscribe(delegate
                {
                    float verticalPull = GlobalDataProxy.GRAVITY * Time.deltaTime * _info.SpeedMultiplier;
                    transform.position += Vector3.down * verticalPull;

                    _targetingLine.SetPosition(0, transform.position);
                });
        }

        public void OnDespawned()
        {
            _fixedUpdateDisposable?.Dispose();
            _pool = null;
        }

        public void Dispose()
        {
            _pool?.Despawn(this);
        }

    }
}
