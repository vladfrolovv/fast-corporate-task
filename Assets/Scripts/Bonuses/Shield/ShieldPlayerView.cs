using DG.Tweening;
using UniRx;
using UnityEngine;
using Zenject;
namespace Bonuses.Shield
{
    public class ShieldPlayerView : MonoBehaviour
    {
        [SerializeField] private Transform _shield;

        private ShieldState _shieldState;
        private float _standardScale;

        [Inject]
        public void Construct(ShieldState shieldState)
        {
            _shieldState = shieldState;
        }

        protected void Awake()
        {
            _standardScale = _shield.localScale.x;
            _shield.localScale = Vector3.zero;
            _shieldState.IsActive.Subscribe(SetShieldActive).AddTo(this);
        }

        private void SetShieldActive(bool active)
        {
            _shield.DOScale(active ? _standardScale : 0, 0.64f).SetEase(Ease.OutBack);
        }

    }
}
