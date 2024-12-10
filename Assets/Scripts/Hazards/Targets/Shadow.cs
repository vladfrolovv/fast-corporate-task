using System;
using UniRx;
using UnityEngine;
using Zenject;
namespace Hazards.Targets
{
    public class Shadow  : MonoBehaviour, IPoolable<ShadowInfo, IMemoryPool>, IDisposable
    {

        [SerializeField] private SpriteRenderer _view;

        private ShadowInfo _info;
        private IMemoryPool _pool;

        private IDisposable _fixedUpdateDisposable;

        public void OnSpawned(ShadowInfo info, IMemoryPool pool)
        {
            _info = info;
            _pool = pool;

            _info.AcidDrop.OnAcidDropDispose += Dispose;

            _fixedUpdateDisposable = Observable
                .EveryUpdate()
                .Select(_ => _info.AcidDrop.transform.position)
                .Subscribe(UpdateShadow).AddTo(this);
        }
        
        private void UpdateShadow(Vector3 fallingPosition)
        {
            float t = fallingPosition.y / _info.FallingObjStartPos.y;
            float scale = Mathf.Lerp(0.25f, 1f, t);
            float alpha = Mathf.Lerp(0.25f, 1f, t);

            transform.localScale = Vector3.one * scale;
            _view.color = new Color(1f, 1f, 1f, alpha);
        }

        public void OnDespawned()
        {
            _info = null;
            _pool = null;
        }

        public void Dispose()
        {
            _info.AcidDrop.OnAcidDropDispose -= Dispose;
            _fixedUpdateDisposable?.Dispose();
            _pool?.Despawn(this);
        }

    }
}
