using UnityEngine;
namespace Hazards.AcidDrops
{
    public class AcidDropInfo
    {
        public AcidDropInfo(Vector3 destinationPoint, float speedMultiplier)
        {
            DestinationPoint = destinationPoint;
            SpeedMultiplier = speedMultiplier;
        }

        public Vector3 DestinationPoint { get; }
        public float SpeedMultiplier { get; }

    }
}
