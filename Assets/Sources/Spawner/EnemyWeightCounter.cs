using UnityEngine;

public class EnemyWeightCounter : MonoBehaviour
{
    private ComplexityCounter _сomplexityCounter;
    private float _weight;
    public EnemyWeightCounter(int wave)
    {
        _сomplexityCounter = new ComplexityCounter(wave);
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
