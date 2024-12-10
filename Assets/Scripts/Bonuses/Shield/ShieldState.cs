using UniRx;
using UnityEngine;
namespace Bonuses.Shield
{
    public class ShieldState : MonoBehaviour
    {
        private readonly ReactiveProperty<bool> _isActive = new(false);
        public IReadOnlyReactiveProperty<bool> IsActive => _isActive;

        public void Activate()
        {
            _isActive.Value = true;
        }

        public void Deactivate()
        {
            _isActive.Value = false;
        }

    }
}
