using UniRx;
using UnityEngine;
namespace Menu.ColorPicker
{
    public class PlayerViewRotator : MonoBehaviour
    {

        [SerializeField] private float _rotationSpeed;

        private readonly CompositeDisposable _compositeDisposable = new();

        protected void Awake()
        {
            Observable.EveryUpdate().Subscribe(delegate
            {
                transform.Rotate(Vector3.up, _rotationSpeed * Time.deltaTime);
            }).AddTo(_compositeDisposable);
        }

        protected void OnDestroy()
        {
            _compositeDisposable?.Dispose();
        }

    }
}
