using UnityEngine;
namespace Players
{
    public class PlayerAnimationController : MonoBehaviour
    {

        private readonly int _isWalkingHash = Animator.StringToHash("IsWalking");
        private readonly int _isRunningHash = Animator.StringToHash("IsRunning");
        private readonly int _isDeadHash = Animator.StringToHash("IsDead");

        [SerializeField] private Animator _animator;

        [SerializeField] private float _movingSpeedThreshold;
        [SerializeField] private float _runningSpeedThreshold;

        public void SetMovingSpeed(float speed)
        {
            _animator.SetBool(_isWalkingHash, speed > _movingSpeedThreshold && speed < _runningSpeedThreshold);
            _animator.SetBool(_isRunningHash, speed > _runningSpeedThreshold);
        }

        public void SetDeathState()
        {
            _animator.SetBool(_isDeadHash, true);
        }

    }
}
