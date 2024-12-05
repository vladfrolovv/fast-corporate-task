using UniRx;
using UnityEngine;
using UnityEngine.InputSystem;
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
                Vector3 input = new (_joystick.Direction.x, 0, _joystick.Direction.y);
                if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
                {
                    input.z = 1;
                }

                if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
                {
                    input.z = -1;
                }

                if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
                {
                    input.x = -1;
                }

                if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
                {
                    input.x = 1;
                }

                SetInput(input);
            });
        }
    }
}
