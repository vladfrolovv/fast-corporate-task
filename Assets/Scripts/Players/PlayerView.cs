using UnityEngine;
namespace Players
{

    public class PlayerView : MonoBehaviour
    {

        [SerializeField] private Collider _collider;

        public Collider Collider => _collider;

        public Vector3 Position
        {
            get => transform.position;
            set => transform.position = value;
        }

        public Quaternion Rotation
        {
            get => transform.rotation;
            set => transform.rotation = value;
        }

    }

}
