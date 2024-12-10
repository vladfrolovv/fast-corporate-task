using System.Collections.Generic;
using DG.Tweening;
using UniRx;
using UnityEngine;
using Zenject;
namespace Bonuses.AcidBoots
{
    public class AcidBootsPlayerView : MonoBehaviour
    {

        [SerializeField] private List<Transform> _boots = new();

        private AcidBootsState _bootsState;
        private float _standardScale;

        [Inject]
        public void Construct(AcidBootsState bootsState)
        {
            _bootsState = bootsState;
        }

        protected void Awake()
        {
            _standardScale = _boots[0].localScale.x;
            _boots.ForEach(b => { b.localScale = Vector3.zero; });
            _bootsState.IsActive.Subscribe(SetBootsActive).AddTo(this);
        }

        private void SetBootsActive(bool active)
        {
            foreach (Transform boot in _boots)
            {
                boot.DOScale(active ? _standardScale : 0, 0.64f).SetEase(Ease.OutBack);
            }
        }
        
    }
}
