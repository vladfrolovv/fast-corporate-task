using UnityEngine;
namespace Players
{
    [CreateAssetMenu(fileName = "PlayerConfig", menuName = "SO/PlayerConfig")]
    public class PlayerConfig : ScriptableObject
    {

        [Header("Movement")]
        [SerializeField] private float _speed = 5f;

        public float Speed => _speed;

    }
}
