using DataProxies;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Zenject;
namespace Players
{
    [RequireComponent(typeof(Slider))]
    public class HealthView : MonoBehaviour
    {

        private HealthDataProxy _healthDataProxy;
        private Slider _slider;

        [Inject]
        public void Construct(HealthDataProxy healthDataProxy)
        {
            _healthDataProxy = healthDataProxy;
        }

        protected void Awake()
        {
            _slider = GetComponent<Slider>();
            _slider.value = GlobalDataProxy.BASE_HEALTH;

            _healthDataProxy.Health.Subscribe(delegate(float h)
            {
                _slider.value = h / GlobalDataProxy.BASE_HEALTH;
            }).AddTo(this);
        }

    }
}
