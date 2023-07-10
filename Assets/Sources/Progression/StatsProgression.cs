using System;
using UnityEngine;

namespace Clones.Progression
{
    public class StatsProgression
    {
        public Stats GetStats(int wave, Stats baseStats)
        {
            float halfCoefficient = (float)(Math.Exp(wave / 10f) / 2);

            //Debug.Log(halfCoefficient);

            if (halfCoefficient < 1)
                halfCoefficient = 1;

            int health = (int)(baseStats.Health * halfCoefficient);
            int damage = (int)(baseStats.Damage * halfCoefficient);

            return new Stats(health, damage, baseStats.AttackSpeed);
        }
    }
}
