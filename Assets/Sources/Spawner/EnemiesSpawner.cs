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
    [SerializeField] private SpawnerData _spawnerData;

    [SerializeField] private PlayerArea _targetArea;
    [SerializeField] private Player _target;

    [SerializeField] private Complexity _complexity;
    [SerializeField] private CurrentBiome _currentBiome;

    public int QuestLevel => _currentWave;

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
        BiomeData targetBiomeData = _currentBiome.BiomeData;
        IReadOnlyList<EnemyData> targetEnemyDatas = targetBiomeData.EnemyDatas;

        if (targetEnemyDatas.Count == 0)
            throw new Exception("enemies to create wave not found");

        _maxWeight = _spawnerData.TotalWeight * _complexity.Value;
        _currentWeight = 0;

        while (_currentWeight < _maxWeight)
        {
            EnemyData currentEnemyData = targetEnemyDatas[Random.Range(0, targetEnemyDatas.Count)];

            Stats stats = GetEnemyStats(currentEnemyData.GetStats());
            float enemyWeight = GetEnemyWeight(stats);

            var enemy = Instantiate(currentEnemyData.EnemyPrefab, GetRandomPositionOutSideScreen(), Quaternion.identity, transform);
            enemy.Init(_target, _targetArea, stats);

            EnemyCreated?.Invoke(enemy);

            _currentWeight += enemyWeight;
        }
    }

    private Stats GetEnemyStats(Stats baseStats)
    {
        float halfResultComplexity = _complexity.Value / 2;

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
        //Vector3 point = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 0));

        Vector3 point = Vector3.zero;
        bool isCorrectPoint = false;
        int i = 0;

        while(isCorrectPoint == false)
        {
            point = Random.insideUnitCircle * _spawnerData.SpawnRadius + new Vector2(_target.transform.position.x, _target.transform.position.z);

            Vector3 viewportPoint = Camera.main.WorldToViewportPoint(point);

            if ((viewportPoint.y < 0 || viewportPoint.y > 2) || (viewportPoint.x < -0.65f || viewportPoint.x > 1.65f))
                isCorrectPoint = true;
            
        }

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
            yield return new WaitForSeconds(_spawnerData.Cooldown);
        }
    }
}
