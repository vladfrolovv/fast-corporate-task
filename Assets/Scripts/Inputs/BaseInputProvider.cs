using UniRx;
using UnityEngine;
namespace Inputs
{
    public abstract class BaseInputProvider : MonoBehaviour
    {

        private readonly ReactiveProperty<Vector3> _inputVector = new (Vector3.zero);

        public IReadOnlyReactiveProperty<Vector3> InputVector => _inputVector;

        protected void SetInput(Vector3 input)
        {
            _inputVector.Value = input;
        }

    }
}
