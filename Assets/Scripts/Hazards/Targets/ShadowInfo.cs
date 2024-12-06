using Hazards.AcidDrops;
using UnityEngine;
namespace Hazards.Targets
{
    public class ShadowInfo
    {

        public ShadowInfo(AcidDrop acidDrop, Vector3 fallingObjStartPos)
        {
            AcidDrop = acidDrop;
            FallingObjStartPos = fallingObjStartPos;
        }

        public AcidDrop AcidDrop { get; }
        public Vector3 FallingObjStartPos { get; }

    }
}
