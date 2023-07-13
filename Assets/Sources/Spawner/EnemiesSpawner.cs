using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;
using Clones.Data;
using Clones.Progression;

public class EnemiesSpawner : MonoBehaviour, IComplexityble
{
    [SerializeField] private EnemyData _enemyData;
    [SerializeField] private SpawnerData _spawnerData;
    [SerializeField] private float _delay;

    [SerializeField] private PlayerArea _targetArea;
    [SerializeField] private Player _target;

    [SerializeField] private Complexity _complexity;

    public int Complexity => _currentWave;

    private float _maxWeight;
    private float _currentWeight;

    private int _currentWave = 0;


    public event Action<Enemy> EnemyCreated;
    public event Action ComplexityIncreased;

    private void Start() => StartCoroutine(Spawner());

    private void CreateWave()
    {
        Stats stats = GetEnemyStats(_enemyData.GetStats());
        float enemyWeight = GetEnemyWeight(stats);

        _maxWeight = _spawnerData.BaseTotalWeight * _complexity.ResultComplexity;
        _currentWeight = 0;

        Debug.Log("max weight " + _maxWeight + " enemy weight " + enemyWeight);

        while (_currentWeight < _maxWeight)
        {
            var enemy = Instantiate(_enemyData.EnemyPrefab, GetRandomPositionOutSideScreen(), Quaternion.identity, transform);
            enemy.Init(_target, _targetArea, stats);

            EnemyCreated?.Invoke(enemy);

            _currentWeight += enemyWeight;
        }
    }

    private Stats GetEnemyStats(Stats baseStats)
    {
        float halfResultComplexity = _complexity.ResultComplexity / 2;

        if (halfResultComplexity < 1)
            halfResultComplexity = 1;

        int health = (int)(baseStats.Health * halfResultComplexity);
        int damage = (int)(baseStats.Damage * halfResultComplexity);

        return new Stats(health, damage, baseStats.AttackSpeed);
    }

    private float GetEnemyWeight(Stats stats)
    {
        return (1 / stats.AttackSpeed) * stats.Damage + (stats.Health / 3);
    }

    private Vector3 GetRandomPositionOutSideScreen()
    {
        Vector2 point = Random.insideUnitCircle.normalized * 20 + new Vector2(_target.transform.position.x, _target.transform.position.z);

        return new Vector3(point.x, 1, point.y);
    }

    private IEnumerator Spawner()
    {
        bool isFinish = false;

        while(isFinish == false)
        {
            _currentWave++;
            ComplexityIncreased?.Invoke();
            CreateWave();
            yield return new WaitForSeconds(_delay);
        }
    }
}
