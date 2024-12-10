using DG.Tweening;
using UniRx;
using UnityEngine;
using Zenject;
namespace Gameplay
{
    public class UICountdownHandler : MonoBehaviour
    {

        private Countdown _countdown;

        [Inject]
        public void Construct(Countdown countdown)
        {
            _countdown = countdown;
        }

        protected void Awake()
        {
            transform.localScale = Vector3.zero;
            _countdown.CountdownEnded.Subscribe(delegate
            {
                transform.DOScale(Vector3.one, .32f);
            }).AddTo(this);
        }

    }
}
