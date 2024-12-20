using System;
using UniRx;
using UnityEngine;
using Zenject;
namespace Gameplay
{
    public class GameManager : MonoBehaviour
    {

        private Countdown _countdown;

        private DateTime _startTime;

        public TimeSpan TimeSinceStart => DateTime.Now - _startTime;

        [Inject]
        public void Construct(Countdown countdown)
        {
            _countdown = countdown;
        }

        protected void Awake()
        {
            _countdown.StartCountdown();
            _countdown.CountdownEnded.Subscribe(delegate
            {
                _startTime = DateTime.Now;
            }).AddTo(this);
        }

    }
}
