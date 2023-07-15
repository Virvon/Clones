using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;
using Clones.Data;
using Clones.Progression;
using Clones.Biomes;
using System.Collections.Generic;

public class EnemiesSpawner : MonoBehaviour, IComplexityble
{
    [SerializeField] private List<EnemyData> _enemyDatas;
    [SerializeField] private SpawnerData _spawnerData;
    [SerializeField] private float _delay;

    [SerializeField] private PlayerArea _targetArea;
    [SerializeField] private Player _target;

    [SerializeField] private Complexity _complexity;
    [SerializeField] private CurrentBiome _currentBiome;

    public int Complexity => _currentWave;

    private float _maxWeight;
    private float _currentWeight;

    private int _currentWave = 0;


    public event Action<Enemy> EnemyCreated;
    public event Action ComplexityIncreased;

    private IEnumerator Start()
    {
        yield return new WaitForSeconds(2);

        StartCoroutine(Spawner());
    }

    private void CreateWave()
    {
        BiomeType targetBiome = _currentBiome.Biome;
        List<EnemyData> targetEnemyDatas = new List<EnemyData>();

        Debug.Log(_currentBiome.Biome);

        foreach(var enemyData in _enemyDatas)
        {
            if (enemyData.BiomeType == targetBiome)
                targetEnemyDatas.Add(enemyData);
        }

        if (targetEnemyDatas.Count == 0)
            throw new Exception("enemies to create wave not found");

        _maxWeight = _spawnerData.BaseTotalWeight * _complexity.ResultComplexity;
        _currentWeight = 0;

        while (_currentWeight < _maxWeight)
        {
            EnemyData currentEnemyData = targetEnemyDatas[Random.Range(0, targetEnemyDatas.Count)];

            Stats stats = currentEnemyData.GetStats();
            float enemyWeight = GetEnemyWeight(stats);

            var enemy = Instantiate(currentEnemyData.EnemyPrefab, GetRandomPositionOutSideScreen(), Quaternion.identity, transform);
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
