using System;

namespace Clones.WandsBuffs
{
    [Serializable]
    public class WandStats
    {
        public int HealthIncreasePercentage;
        public int DamageIncreasePercentage;
        public int AttackCooldwonDecreasePercentage;
        public int PreyResourcesIncreasePercentage;

        public static WandStats operator +(WandStats a, WandStatsIncrease b)
        {
            return new WandStats()
            {
                HealthIncreasePercentage = a.HealthIncreasePercentage + b.HealthIncrease,
                DamageIncreasePercentage = a.DamageIncreasePercentage + b.DamageIncrease,
                AttackCooldwonDecreasePercentage = a.AttackCooldwonDecreasePercentage + b.AttackCooldownDecrease,
                PreyResourcesIncreasePercentage = a.PreyResourcesIncreasePercentage + b.PreyResourcesIncrease
            };
        }
    }
}
