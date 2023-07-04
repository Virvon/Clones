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
        float coefficient = (float)Math.Pow(e, (float)_wave / 12.5f);

        int damage = (int)(Config.BaseEnemyDamage * coefficient);
        int health = (int)(Config.BaseEnemyHealth * coefficient);
        Debug.Log("enemy coefficient " + coefficient);

        return new Stats(health, damage, Config.BaseEnemyAttackSpeed);
    }

    public float GetTotalWeight()
    {
        float coefficient = (float)Math.Pow(e, (float)_wave / 6f);
        Debug.Log("weight coefficient " + coefficient);

        return (float)(Config.BaseTotalWeight * coefficient);
    }
}
