using Players;
using UniRx;
using UnityEngine;
using Zenject;
namespace Camera
{
    public class PlayerCameraFollower : MonoBehaviour
    {

        private PlayerView _playerView;

        [SerializeField] private float _interpolator = .1f;

        [Inject]
        public void Construct(PlayerView playerView)
        {
            _playerView = playerView;
        }

        protected void Awake()
        {
            Observable.EveryUpdate().Subscribe(delegate
            {
                Vector3 smoothFollow = Vector3.Lerp(transform.position, _playerView.Position, _interpolator);
                transform.position = smoothFollow;
            });
        }

    }
}
