using UnityEngine;
namespace Hazards
{
    [RequireComponent(typeof(BoxCollider))]
    public class AcidZone : MonoBehaviour
    {

        private BoxCollider _collider;

        protected void Awake()
        {
            _collider = GetComponent<BoxCollider>();
        }

        public Vector3 GetPointInZone()
        {
            Vector3 extents = _collider.size / 2f;
            Vector3 point = new (
                Random.Range(-extents.x, extents.x),
                Random.Range(-extents.y, extents.y),
                Random.Range(-extents.z, extents.z)
            );

            return _collider.transform.TransformPoint(point);
        }

    }
}
