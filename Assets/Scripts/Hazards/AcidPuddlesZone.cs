using UniRx;
using UniRx.Triggers;
using UnityEngine;
namespace Hazards
{
    public class AcidPuddlesZone : MonoBehaviour
    {

        private BoxCollider _collider;

        private readonly CompositeDisposable _compositeDisposable = new();

        protected void Awake()
        {
            _collider = GetComponent<BoxCollider>();
            _collider.OnTriggerEnterAsObservable().Subscribe(delegate(Collider collider)
            {
                if (collider.TryGetComponent(out AcidDrop acidDrop))
                {
                    acidDrop.Dispose();
                }
            });
        }

    }
}
