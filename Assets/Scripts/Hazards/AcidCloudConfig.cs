using UnityEngine;
namespace Hazards
{
    [CreateAssetMenu(fileName = "AcidCloudConfig", menuName = "SO/AcidCloudConfig")]
    public class AcidCloudConfig : ScriptableObject
    {

        [SerializeField] private float _raindropsDelay;

        public float RaindropsDelay => _raindropsDelay;

    }
}
