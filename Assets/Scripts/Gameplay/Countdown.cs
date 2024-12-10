using System;
using UniRx;
using UnityEngine;
namespace Gameplay
{
    public class Countdown : MonoBehaviour
    {
        private readonly Subject<bool> _countdownEnded = new();

        public IObservable<bool> CountdownEnded => _countdownEnded;

        public void StartCountdown()
        {
            Observable
                .Timer(TimeSpan.FromSeconds(5f))
                .Subscribe(delegate(long l)
                {
                    _countdownEnded?.OnNext(true);
                }).AddTo(this);
        }
    }
}
