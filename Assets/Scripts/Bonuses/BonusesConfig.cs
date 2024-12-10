using System;
using UnityEngine;
namespace Bonuses
{
    [CreateAssetMenu(fileName = "BonusesConfig", menuName = "SO/BonusesConfig")]
    public class BonusesConfig : ScriptableObject
    {

        [Header("Shield")]
        [SerializeField] private float _shieldDuration;
        [SerializeField] private float _shieldCooldown;

        [Header("Acid Boots")]
        [SerializeField] private float _acidBootsDuration;
        [SerializeField] private float _acidBootsCooldown;

        public float ShieldDuration => _shieldDuration;
        public float ShieldCooldown => _shieldCooldown;

        public float AcidBootsDuration => _acidBootsDuration;
        public float AcidBootsCooldown => _acidBootsCooldown;

    }
}
