using Clones.Infrastructure;
using Clones.Services;
using Clones.StaticData;
using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Clones.GameLogic
{
    public class EnemiesSpawner : IEnemiesSpawner
    {
        private const float StartDelay = 3;
        private const float SpawnCooldown = 12;
        private const float MaxWeight = 10;
        private const float MinRadius = 10;
        private const float MaxRadius = 20;

        private readonly ICoroutineRunner _coroutineRunner;
        private readonly ICurrentBiome _currentBiome;
        private readonly IStaticDataService _staticDataService;
        private readonly Transform _player;
        private readonly IGameFactory _gameFactory;

        private bool _isFinish;

        public EnemiesSpawner(ICoroutineRunner coroutineRunner, ICurrentBiome currentBiome, IStaticDataService staticDataService, Transform player, IGameFactory gameFactory)
        {
            _coroutineRunner = coroutineRunner;
            _currentBiome = currentBiome;
            _staticDataService = staticDataService;
            _player = player;
            _gameFactory = gameFactory;
        }


        public void Start()
        {
            _isFinish = false;

            _coroutineRunner.StartCoroutine(Spawner());
        }

        public void Stop()
        {
            _isFinish = true;
        }

        private void CreateWave()
        {
            EnemyType[] spawnedEnemies = GetSpawnedEnemies();

            if (spawnedEnemies.Length == 0)
                throw new Exception("enemies to create wave not found");

            float currentWeight = 0;

            while(currentWeight < MaxWeight)
            {
                EnemyType spawnedEnemy = GetRandomEnemyType(spawnedEnemies);
                Vector3 spawnPosition = GetSpawnPosition();

                _gameFactory.CreateEnemy(spawnedEnemy, spawnPosition, Quaternion.identity, out float weight);

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

            while(_isFinish == false)
            {
                CreateWave();

                yield return new WaitForSeconds(SpawnCooldown);
            }
        }
    }
}