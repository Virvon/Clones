using Clones.GameLogic;
using Clones.Infrastructure;
using Clones.Types;
using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Clones.EducationLogic
{
    public class EducationEnemiesSpawner : MonoBehaviour, IDestoryableEnemies
    {
        private WaveInfo[] _waveInfos;
        private int _waveNumber;
        private IPartsFactory _partsFactory;
        private GameObject _playerObject;

        public void Init(WaveInfo[] waveInfos, IPartsFactory partsFactory, GameObject playerObject)
        {
            _waveInfos = waveInfos;
            _partsFactory = partsFactory;
            _playerObject = playerObject;

            _waveNumber = 0;
        }

        public void Spawn()
        {
            CreateWave();
            _waveNumber++;
        }

        public void DestroyExistingEnemies()
        {
            foreach (EnemyHealth enemy in GetComponentsInChildren<EnemyHealth>())
                enemy.Disappear();
        }

        private void CreateWave()
        {
            WaveInfo waveInfo = _waveInfos[_waveNumber];
            EnemyType[] spawnedEnemies = waveInfo.SpawnedEnemies;

            if (spawnedEnemies.Length == 0)
                throw new Exception("enemies to create wave not found");

            float maxWeight = waveInfo.WaveWeight * waveInfo.Complexity;
            float currentWeight = 0;

            while (currentWeight < maxWeight)
            {
                EnemyType spawnedEnemy = GetRandomEnemyType(spawnedEnemies);
                Vector3 spawnPosition = GetSpawnPosition(waveInfo);

                _partsFactory.CreateEnemy(spawnedEnemy, spawnPosition, Quaternion.identity, transform, out float weight, _playerObject, waveInfo.Complexity);

                currentWeight += weight;
            }
        }

        private EnemyType GetRandomEnemyType(EnemyType[] typeOptions) =>
            typeOptions[Random.Range(0, typeOptions.Length)];

        private Vector3 GetSpawnPosition(WaveInfo waveInfo)
        {
            float distance = Random.Range(waveInfo.MinSpawnRadius, waveInfo.MaxSpawnRadius + 1);
            Vector2 point = Random.insideUnitCircle.normalized * distance + waveInfo.Position;

            return new Vector3(point.x, 0, point.y);
        }
    }
}
