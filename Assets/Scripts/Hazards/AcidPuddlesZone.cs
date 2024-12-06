using Hazards.AcidDrops;
using Hazards.Particles;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using Zenject;
namespace Hazards
{
    public class AcidPuddlesZone : MonoBehaviour
    {

        private BoxCollider _collider;
        private AcidParticlesFactory _acidParticlesFactory;

        private readonly CompositeDisposable _compositeDisposable = new();

        [Inject]
        public void Construct(AcidParticlesFactory acidParticlesFactory)
        {
            _acidParticlesFactory = acidParticlesFactory;
        }

        protected void Awake()
        {
            _collider = GetComponent<BoxCollider>();
            _collider.OnTriggerEnterAsObservable().Subscribe(delegate(Collider collider)
            {
                if (collider.TryGetComponent(out AcidDrop acidDrop))
                {

                    AcidParticles acidParticles = _acidParticlesFactory.Create(new AcidParticlesInfo());
                    acidParticles.transform.position = acidDrop.transform.position;

                    acidDrop.OnHit();
                }
            });
        }

    }
}
