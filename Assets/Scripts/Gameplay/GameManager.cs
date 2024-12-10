using UnityEngine;
using Zenject;
namespace Gameplay
{
    public class GameManager : MonoBehaviour
    {

        private Countdown _countdown;

        [Inject]
        public void Construct(Countdown countdown)
        {
            _countdown = countdown;
        }

        protected void Awake()
        {
            _countdown.StartCountdown();
        }

    }
}
