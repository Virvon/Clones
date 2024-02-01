using System;
using UnityEngine;

namespace Clones.WandsBuffs
{
    [Serializable]
    public class WandStats
    {
        public int HealthIncreasePercentage;
        public int DamageIncreasePercentage;
        [Range(0, 99)] public int AttackCooldownDecreasePercentage;
        public int PreyResourcesIncreasePercentage;
        [Range(0, 99)] public int MaxAttackCooldownDecreasePercentage; 

        public static WandStats operator +(WandStats a, WandStatsIncrease b)
        {
            return new WandStats()
            {
                HealthIncreasePercentage = a.HealthIncreasePercentage + b.HealthIncrease,
                DamageIncreasePercentage = a.DamageIncreasePercentage + b.DamageIncrease,
                AttackCooldownDecreasePercentage = a.AttackCooldownDecreasePercentage + b.AttackCooldownDecrease <= a.MaxAttackCooldownDecreasePercentage? a.AttackCooldownDecreasePercentage + b.AttackCooldownDecrease : a.MaxAttackCooldownDecreasePercentage,
                PreyResourcesIncreasePercentage = a.PreyResourcesIncreasePercentage + b.PreyResourcesIncrease,
                MaxAttackCooldownDecreasePercentage = a.MaxAttackCooldownDecreasePercentage
            };
        }
    }
}