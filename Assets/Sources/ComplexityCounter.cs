using System;
using Clones.Data;

public class ComplexityCounter
{
    private const float e = 2.72f;

    private int _wave;
    private Stats _baseStats;
    private float _baseTotalWeght;

    public ComplexityCounter(int wave, EnemyData enemyData, float baseTotalWeght)
    {
        _wave = wave;
        _baseStats = enemyData.GetStats();
        _baseTotalWeght = baseTotalWeght;
    }

    public Stats GetEnemyStats()
    {
        float coefficient = (float)Math.Pow(e, (float)_wave / 12.5f);

        int damage = (int)(_baseStats.Damage * coefficient);
        int health = (int)(_baseStats.Health * coefficient);

        return new Stats(health, damage, _baseStats.AttackSpeed);
    }

    public float GetTotalWeight()
    {
        float coefficient = (float)Math.Pow(e, (float)_wave / 6f);

        return (float)(_baseTotalWeght * coefficient);
    }
}
