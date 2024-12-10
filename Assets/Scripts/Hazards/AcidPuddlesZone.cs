using Hazards.AcidDrops;
using Hazards.Particles;
using Hazards.Puddles;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using Zenject;
namespace Hazards
{
    [RequireComponent(typeof(BoxCollider))]
    public class AcidPuddlesZone : MonoBehaviour
    {

        private BoxCollider _collider;

        private PuddlesConfig _puddlesConfig;
        private PuddlesFactory _puddlesFactory;
        private AcidParticlesFactory _acidParticlesFactory;

        private int _dropsCount;

        [Inject]
        public void Construct(AcidParticlesFactory acidParticlesFactory, PuddlesFactory puddlesFactory,
                              PuddlesConfig puddlesConfig)
        {
            _acidParticlesFactory = acidParticlesFactory;
            _puddlesFactory = puddlesFactory;
            _puddlesConfig = puddlesConfig;
        }

        protected void Awake()
        {
            _collider = GetComponent<BoxCollider>();
            _collider.OnTriggerEnterAsObservable().Subscribe(delegate(Collider collider)
            {
                collider.TryGetComponent(out AcidDrop acidDrop);
                if (acidDrop == null) return;

                _dropsCount++;

                if (_dropsCount % _puddlesConfig.SpawnRate == 0)
                {
                    Vector3 acidDropPosition = acidDrop.transform.position;
                    Puddle puddle = _puddlesFactory.Create(new PuddleInfo(_puddlesConfig.TimeToLive));
                    puddle.transform.position = new Vector3(acidDropPosition.x, 0, acidDropPosition.z);
                }

                AcidParticles acidParticles = _acidParticlesFactory.Create(
                    new AcidParticlesInfo(ParticlesType.Acid));
                acidParticles.transform.position = acidDrop.transform.position;

                acidDrop.OnHit();
            }).AddTo(this);
        }

    }
}
