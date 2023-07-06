using UnityEngine;
using Clones.Data;

public class EnemyWeightCounter
{
    private ComplexityCounter _сomplexityCounter;
    private float _weight;

    public EnemyWeightCounter(int wave,EnemyData enemyData, float baseTotalWeight)
    {
        _сomplexityCounter = new ComplexityCounter(wave, enemyData, baseTotalWeight);
        _weight = 0;
    }

    public bool TryGetEnemyStats(out Stats enemyStats)
    {
        enemyStats = _сomplexityCounter.GetEnemyStats();

        _weight += GetEnemyWeight(enemyStats);

        if (_weight <= _сomplexityCounter.GetTotalWeight())
            return true;

        enemyStats = null;
        return false;
    }

    private float GetEnemyWeight(Stats enemyStats)
    {
        return enemyStats.AttackSpeed * enemyStats.Damage * enemyStats.Health;
    }
}
