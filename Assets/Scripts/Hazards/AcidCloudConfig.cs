using UnityEngine;
namespace Hazards
{
    [CreateAssetMenu(fileName = "AcidCloudConfig", menuName = "SO/AcidCloudConfig")]
    public class AcidCloudConfig : ScriptableObject
    {

        [SerializeField] private float _raindropsDelay;
        [SerializeField] private float _raindropSpeedMultiplier;

        public float RaindropsDelay => _raindropsDelay;
        public float RaindropSpeedMultiplier => _raindropSpeedMultiplier;

    }
}
