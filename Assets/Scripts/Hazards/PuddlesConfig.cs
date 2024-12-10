using UnityEngine;
namespace Hazards
{
    [CreateAssetMenu(fileName = "PuddlesConfig", menuName = "SO/PuddlesConfig")]
    public class PuddlesConfig : ScriptableObject
    {

        [Header("General")]
        [SerializeField] private float _timeToLive;
        [SerializeField] private float _spawnRate;

        [Header("Puddle")]
        [SerializeField] private float _damageTick;
        [SerializeField] private float _speedMultiplier;

        public float TimeToLive => _timeToLive;
        public float SpawnRate => _spawnRate;

        public float DamageTick => _damageTick;
        public float SpeedMultiplier => _speedMultiplier;

    }
}
