using DG.Tweening;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Zenject;
namespace Bonuses.Shield
{
    public class ShieldUIView : MonoBehaviour
    {

        [SerializeField] private Button _button;
        [SerializeField] private Image _uiView;

        private ShieldState _shieldState;
        private BonusesConfig _bonusesConfig;

        [Inject]
        public void Construct(ShieldState shieldState, BonusesConfig bonusesConfig)
        {
            _shieldState = shieldState;
            _bonusesConfig = bonusesConfig;
        }

        protected void Awake()
        {
            _button.OnClickAsObservable().Subscribe(delegate
            {
                _button.interactable = false;

                _shieldState.Activate();
                StartTimer();
            }).AddTo(this);
        }

        private void StartTimer()
        {
            _uiView.DOFillAmount(0, _bonusesConfig.ShieldDuration);
            Observable.Timer(System.TimeSpan.FromSeconds(_bonusesConfig.ShieldDuration)).Subscribe(delegate
            {
                _shieldState.Deactivate();
                StartCooldown();
            }).AddTo(this);
        }

        private void StartCooldown()
        {
            _uiView.DOFillAmount(1, _bonusesConfig.ShieldCooldown);
            Observable.Timer(System.TimeSpan.FromSeconds(_bonusesConfig.ShieldCooldown)).Subscribe(delegate
            {
                _button.interactable = true;
            }).AddTo(this);
        }
    }
}
