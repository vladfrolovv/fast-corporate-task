using System;
using DataProxies;
using Hazards.AcidDrops;
using Inputs;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
namespace Players
{
    public class PlayerPresenter : IDisposable
    {

        private readonly CompositeDisposable _compositeDisposable = new ();

        private Vector3 _movementVector = Vector3.zero;

        public PlayerPresenter(PlayerView playerView, PlayerConfig playerConfig, BaseInputProvider baseInputProvider,
                               PlayerAnimationController playerAnimationController)
        {
            baseInputProvider.InputVector.Subscribe(delegate(Vector3 inputVector)
            {
                _movementVector = Quaternion.AngleAxis(GlobalDataProxy.WORLD_ROTATION, Vector3.up) * inputVector;
                playerAnimationController.SetMovingSpeed(_movementVector.magnitude);
            }).AddTo(_compositeDisposable);

            Observable.EveryUpdate().Subscribe(delegate
            {
                playerView.Position += _movementVector * playerConfig.Speed * Time.deltaTime;
                playerView.Rotation = _movementVector != Vector3.zero ?
                    Quaternion.LookRotation(_movementVector) :
                    playerView.Rotation;
            }).AddTo(_compositeDisposable);

            playerView.Collider.OnTriggerEnterAsObservable().Subscribe(delegate(Collider collider)
            {
                if (collider.TryGetComponent(out AcidDrop acidDrop))
                {
                    acidDrop.OnHit();
                }
            }).AddTo(_compositeDisposable);
        }

        public void Dispose()
        {
            _compositeDisposable?.Dispose();
        }

    }
}
