using Clones.GameLogic;
using Clones.Infrastructure;
using Clones.Services;
using Clones.StaticData;
using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemiesSpawner : MonoBehaviour
{
    private const float StartDelay = 3;
    private const float SpawnCooldown = 12;
    private const float MaxWeight = 10;
    private const float MinRadius = 10;
    private const float MaxRadius = 20;

    private ICurrentBiome _currentBiome;
    private IStaticDataService _staticDataService;
    private Transform _player;
    private IGameFactory _gameFactory;

    private bool _isFinish;

    private void OnDisable() => 
        _isFinish = true;

    public void Init(ICurrentBiome currentBiome, IStaticDataService staticDataService, Transform player, IGameFactory gameFactory)
    {
        _currentBiome = currentBiome;
        _staticDataService = staticDataService;
        _player = player;
        _gameFactory = gameFactory;

        _isFinish = false;

        StartCoroutine(Spawner());
    }

    private void CreateWave()
    {
        EnemyType[] spawnedEnemies = GetSpawnedEnemies();

        if (spawnedEnemies.Length == 0)
            throw new Exception("enemies to create wave not found");

        float currentWeight = 0;

        while (currentWeight < MaxWeight)
        {
            EnemyType spawnedEnemy = GetRandomEnemyType(spawnedEnemies);
            Vector3 spawnPosition = GetSpawnPosition();

            _gameFactory.CreateEnemy(spawnedEnemy, spawnPosition, Quaternion.identity, transform, out float weight);

            currentWeight += weight;
        }
    }

    private Vector3 GetSpawnPosition()
    {
        float distance = Random.Range(MinRadius, MaxRadius + 1);
        Vector2 point = Random.insideUnitCircle.normalized * distance + new Vector2(_player.transform.position.x, _player.transform.position.z);

        return new Vector3(point.x, 0, point.y);
    }

    private EnemyType GetRandomEnemyType(EnemyType[] typeOptions)
    {
        return typeOptions[Random.Range(0, typeOptions.Length)];
    }

    private EnemyType[] GetSpawnedEnemies()
    {
        BiomeStaticData biomeData = _staticDataService.GetBiomeStaticData(_currentBiome.Type);

        return biomeData.EnemiesTemplated;
    }

    private IEnumerator Spawner()
    {
        yield return new WaitForSeconds(StartDelay);

        while (_isFinish == false)
        {
            CreateWave();

            yield return new WaitForSeconds(SpawnCooldown);
        }
    }
}