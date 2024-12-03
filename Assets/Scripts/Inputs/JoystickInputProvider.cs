using UniRx;
using UnityEngine;
namespace Inputs
{
    [RequireComponent(typeof(Joystick))]
    public class JoystickInputProvider : BaseInputProvider
    {

        private Joystick _joystick;

        protected void Awake()
        {
            _joystick = GetComponent<Joystick>();

            Observable.EveryUpdate().Subscribe(delegate
            {
                SetInput(new Vector3(_joystick.Direction.x, 0, _joystick.Direction.y));
            });
        }

    }
}
