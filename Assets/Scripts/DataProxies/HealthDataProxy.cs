using UniRx;
namespace DataProxies
{
    public class HealthDataProxy
    {

        private readonly ReactiveProperty<float> _health = new(GlobalDataProxy.BASE_HEALTH);

        public IReadOnlyReactiveProperty<float> Health => _health;

        public void TryToDamage(float damage)
        {
            _health.Value -= damage;
        }

        public void Reset()
        {
            _health.Value = GlobalDataProxy.BASE_HEALTH;
        }

    }
}
