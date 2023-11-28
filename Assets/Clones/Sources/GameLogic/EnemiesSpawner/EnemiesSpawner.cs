using Clones.Infrastructure;
using Clones.Services;
using Clones.StaticData;
using Clones.Types;
using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Clones.GameLogic
{
    public class EnemiesSpawner : MonoBehaviour, ITimeScalable
    {
        private float _startDelay = 3;
        private float _spawnCooldown = 12;
        private float _maxWeight = 10;
        private float _minRadius = 10;
        private float _maxRadius = 20;

        private ICurrentBiome _currentBiome;
        private IGameStaticDataService _staticDataService;
        private GameObject _playerObject;
        private IPartsFactory _partsFactory;
        private float _timeScale = 1;

        private bool _isFinish;

        private void OnDisable() =>
            _isFinish = true;

        public void Init(ICurrentBiome currentBiome, IGameStaticDataService staticDataService, GameObject playerObject, float startDelay, float spawnCooldown, float maxWeight)
        {
            _currentBiome = currentBiome;
            _staticDataService = staticDataService;
            _playerObject = playerObject;
            _startDelay = startDelay;
            _spawnCooldown = spawnCooldown;
            _maxWeight = maxWeight;
        }

        public void Init(IPartsFactory partsFactory) =>
            _partsFactory = partsFactory;

        public void StartSpawn()
        {
            _isFinish = false;

            StartCoroutine(Spawner());
        }

        public void ScaleTime(float scale) => 
            _timeScale = scale >= 0 ? scale : 0;

        public void DestroyExistingEnemies()
        {
            foreach (EnemyHealth enemy in GetComponentsInChildren<EnemyHealth>())
                enemy.Disappear();
        }

        private void CreateWave()
        {
            EnemyType[] spawnedEnemies = GetSpawnedEnemies();

            if (spawnedEnemies.Length == 0)
                throw new Exception("enemies to create wave not found");

            float currentWeight = 0;

            while (currentWeight < _maxWeight)
            {
                EnemyType spawnedEnemy = GetRandomEnemyType(spawnedEnemies);
                Vector3 spawnPosition = GetSpawnPosition();

                _partsFactory.CreateEnemy(spawnedEnemy, spawnPosition, Quaternion.identity, transform, out float weight, _playerObject);

                currentWeight += weight;
            }
        }

        private Vector3 GetSpawnPosition()
        {
            float distance = Random.Range(_minRadius, _maxRadius + 1);
            Vector2 point = Random.insideUnitCircle.normalized * distance + new Vector2(_playerObject.transform.position.x, _playerObject.transform.position.z);

            return new Vector3(point.x, 0, point.y);
        }

        private EnemyType GetRandomEnemyType(EnemyType[] typeOptions) => 
            typeOptions[Random.Range(0, typeOptions.Length)];

        private EnemyType[] GetSpawnedEnemies()
        {
            BiomeStaticData biomeData = _staticDataService.GetBiomeStaticData(_currentBiome.Type);

            return biomeData.EnemiesTemplated;
        }

        private IEnumerator Spawner()
        {
            yield return new WaitForSeconds(_startDelay);

            while (_isFinish == false)
            {
                if(_timeScale == 0)
                    yield return new WaitWhile(() => _timeScale == 0);

                CreateWave();

                yield return new WaitForSeconds(_spawnCooldown / _timeScale);
            }
        }
    }
}