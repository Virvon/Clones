using System;
using UnityEngine;

public class ComplexityCounter
{
    private const float e = 2.72f;

    private int _wave;

    public ComplexityCounter(int wave)
    {
        _wave = wave;
    }

    public Stats GetEnemyStats()
    {
        float coefficient = (float)Math.Pow(e, _wave / 8);

        int damage = (int)(Config.BaseEnemyDamage * coefficient);
        int health = (int)(Config.BaseEnemyHealth * coefficient);
        Debug.Log("coefficient " + coefficient);

        return new Stats(health, damage, Config.BaseEnemyAttackSpeed);
    }

    public float GetTotalWeight()
    {
        float coefficient = (float)Math.Pow(e, _wave / 6);

        return (float)(Config.BaseTotalWeight * coefficient);
    }
}
