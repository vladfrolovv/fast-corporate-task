using System;
using DG.Tweening;
using UniRx;
using UnityEngine;
using Zenject;
namespace Hazards.Puddles
{
    public class Puddle : MonoBehaviour, IPoolable<PuddleInfo, IMemoryPool>, IDisposable
    {

        private IMemoryPool _pool;
        private PuddleInfo _info;

        public void OnSpawned(PuddleInfo info, IMemoryPool pool)
        {
            _info = info;
            _pool = pool;

            transform.localScale = Vector3.zero;

            AppearAnimation();

            Observable.Timer(TimeSpan.FromSeconds(info.TimeToLive)).Subscribe(delegate
            {
                DisappearAnimation();
            }).AddTo(this);
        }

        private void AppearAnimation()
        {
            Sequence sequence = DOTween.Sequence();
            sequence.Append(transform.DORotate(
                new Vector3(0f, UnityEngine.Random.Range(180f, 360f), 0f), 1f, RotateMode.FastBeyond360));
            sequence.Join(transform.DOScale(Vector3.one, 1f));

            sequence.Play();
        }

        private void DisappearAnimation()
        {
            Sequence sequence = DOTween.Sequence();
            sequence.Append(transform.DORotate(
                Vector3.zero, 1f, RotateMode.FastBeyond360));
            sequence.Join(transform.DOScale(Vector3.zero, 1f));
            sequence.AppendCallback(delegate
            {
                _pool.Despawn(this);
            });

            sequence.Play();
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
