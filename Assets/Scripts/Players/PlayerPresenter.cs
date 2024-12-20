﻿using System;
using Bonuses.AcidBoots;
using Bonuses.Shield;
using DataProxies;
using Gameplay;
using Hazards;
using Hazards.AcidDrops;
using Hazards.Particles;
using Hazards.Puddles;
using Inputs;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
namespace Players
{
    public class PlayerPresenter : IDisposable
    {

        private readonly CompositeDisposable _compositeDisposable = new ();

        private readonly HealthDataProxy _healthDataProxy;
        private readonly BaseInputProvider _baseInputProvider;
        private readonly PlayerAnimationController _playerAnimationController;
        private readonly PlayerView _playerView;
        private readonly PlayerConfig _playerConfig;
        private readonly AcidParticlesFactory _acidParticlesFactory;
        private readonly PuddlesConfig _puddlesConfig;
        private readonly EndScreen _endScreen;

        private readonly AcidBootsState _acidBootsState;
        private readonly ShieldState _shieldState;

        private IDisposable _puddleDamageDisposable;

        private Vector3 _movementVector = Vector3.zero;
        private bool _isInsidePuddle;

        public PlayerPresenter(PlayerView playerView, PlayerConfig playerConfig, BaseInputProvider baseInputProvider,
                               PlayerAnimationController playerAnimationController, AcidParticlesFactory acidParticlesFactory,
                               HealthDataProxy healthDataProxy, Countdown countdown, PuddlesConfig puddlesConfig,
                               EndScreen endScreen, AcidBootsState acidBootsState, ShieldState shieldState)
        {
            _baseInputProvider = baseInputProvider;
            _playerAnimationController = playerAnimationController;
            _playerView = playerView;
            _playerConfig = playerConfig;
            _healthDataProxy = healthDataProxy;
            _acidParticlesFactory = acidParticlesFactory;
            _puddlesConfig = puddlesConfig;
            _endScreen = endScreen;
            _acidBootsState = acidBootsState;
            _shieldState = shieldState;

            countdown.CountdownEnded.Subscribe(delegate
            {
                MovementHandler();

                playerView.OnTriggerEnterAsObservable().Subscribe(TriggerEnterHandler).AddTo(_compositeDisposable);
                playerView.OnTriggerExitAsObservable().Subscribe(TriggerExitHandler).AddTo(_compositeDisposable);
            }).AddTo(_compositeDisposable);
        }

        private void MovementHandler()
        {
            _baseInputProvider.InputVector.Subscribe(delegate(Vector3 inputVector)
            {
                _movementVector = Quaternion.AngleAxis(GlobalDataProxy.WORLD_ROTATION, Vector3.up) * inputVector;
                _playerAnimationController.SetMovingSpeed(_movementVector.magnitude);
            }).AddTo(_compositeDisposable);

            Observable.EveryUpdate().Subscribe(delegate
            {
                float speedMultiplier = 1f;
                if (_isInsidePuddle && !_acidBootsState.IsActive.Value)
                {
                    speedMultiplier = _puddlesConfig.SpeedMultiplier;
                }

                _playerView.Position += _movementVector * _playerConfig.Speed * Time.deltaTime * speedMultiplier;
                _playerView.Rotation = _movementVector != Vector3.zero ?
                    Quaternion.LookRotation(_movementVector) :
                    _playerView.Rotation;
            }).AddTo(_compositeDisposable);
        }

        private void TriggerEnterHandler(Collider collider)
        {
            if (collider.TryGetComponent(out AcidDrop acidDrop))
            {
                if (_shieldState.IsActive.Value) { return; }

                acidDrop.OnHit();

                CreateBloodParticleOn(acidDrop.transform.position);
                TryToHit();
            }
            else if (collider.TryGetComponent(out Puddle puddle))
            {
                if (_acidBootsState.IsActive.Value) { return; }
                _isInsidePuddle = true;

                PuddleDamageHandler();

                CreateBloodParticleOn(_playerView.transform.position);
                TryToHit();
            }
        }

        private void TriggerExitHandler(Collider collider)
        {
            if (collider.TryGetComponent(out Puddle _))
            {
                _isInsidePuddle = false;
                _puddleDamageDisposable?.Dispose();
            }
        }

        private void PuddleDamageHandler()
        {
            _puddleDamageDisposable = Observable
                .Interval(TimeSpan.FromSeconds(_puddlesConfig.DamageTick))
                .Subscribe(delegate
                {
                    CreateBloodParticleOn(_playerView.transform.position);
                    TryToHit();
                }).AddTo(_compositeDisposable);
        }

        private void TryToHit()
        {
            _healthDataProxy.TryToDamage(1f);
            LooseConditionCheck();
        }

        private void LooseConditionCheck()
        {
            if (_healthDataProxy.Health.Value > 0) return;

            _endScreen.Show();
            _compositeDisposable?.Dispose();
            _playerAnimationController.SetDeathState();
        }

        private void CreateBloodParticleOn(Vector3 position)
        {
            AcidParticles acidParticles = _acidParticlesFactory.Create(new AcidParticlesInfo(ParticlesType.Blood));
            acidParticles.transform.position = position;
        }

        public void Dispose()
        {
            _compositeDisposable?.Dispose();
        }

    }
}
