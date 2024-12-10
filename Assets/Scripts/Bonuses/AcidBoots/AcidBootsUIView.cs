using DG.Tweening;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Zenject;
namespace Bonuses.AcidBoots
{
    public class AcidBootsUIView : MonoBehaviour
    {

        [SerializeField] private Button _button;
        [SerializeField] private Image _uiView;

        private AcidBootsState _acidBootsState;
        private BonusesConfig _bonusesConfig;

        [Inject]
        public void Construct(AcidBootsState acidBootsState, BonusesConfig bonusesConfig)
        {
            _acidBootsState = acidBootsState;
            _bonusesConfig = bonusesConfig;
        }

        protected void Awake()
        {
            _button.OnClickAsObservable().Subscribe(delegate
            {
                _button.interactable = false;

                _acidBootsState.Activate();
                StartTimer();
            }).AddTo(this);
        }

        private void StartTimer()
        {
            _uiView.DOFillAmount(0, _bonusesConfig.AcidBootsDuration);
            Observable.Timer(System.TimeSpan.FromSeconds(_bonusesConfig.AcidBootsDuration)).Subscribe(delegate
            {
                _acidBootsState.Deactivate();
                StartCooldown();
            }).AddTo(this);
        }

        private void StartCooldown()
        {
            _uiView.DOFillAmount(1, _bonusesConfig.AcidBootsCooldown);
            Observable.Timer(System.TimeSpan.FromSeconds(_bonusesConfig.AcidBootsCooldown)).Subscribe(delegate
            {
                _button.interactable = true;
            }).AddTo(this);
        }

    }
}
