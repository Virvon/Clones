using Clones.StaticData;
using UnityEngine;
using Clones.Services;
using Clones.GameLogic;
using System;
using Object = UnityEngine.Object;
using Clones.Biomes;
using UnityEngine.AI;
using Random = UnityEngine.Random;
using Clones.Types;

namespace Clones.Infrastructure
{
    public class PartsFactory : IPartsFactory
    {
        private readonly IGameStaticDataService _staticData;

        public PartsFactory(IGameStaticDataService staticData)
        {
            _staticData = staticData;
        }

        public void CreateEnemy(EnemyType type, Vector3 position, Quaternion rotation, Transform parent, out float weight, GameObject playerObject)
        {
            EnemyStaticData enemyData = _staticData.GetEnemyStaticData(type);

            weight = GetEnemyWeight(enemyData);

            var enemyObject = Object.Instantiate(enemyData.Prefab, position, rotation, parent);

            enemyObject.GetComponent<Enemy>()
                .Init(playerObject);

            enemyObject.GetComponent<NavMeshAgent>()
                .stoppingDistance = (float)Math.Round(Random.Range(enemyData.MinStopDistance, enemyData.MaxStopDistance), 2);

            enemyObject.GetComponent<MeleeAttack>()
                .Init(enemyData.Damage, enemyData.AttackCooldown);

            EnemyHealth enemyHealth = enemyObject.GetComponent<EnemyHealth>();
            enemyHealth.Init(enemyData.Health);

            enemyObject.GetComponentInChildren<EnemyHealthbar>()
                .Init(enemyHealth);

            enemyObject.GetComponent<DamageableDeath>()
                .Init(enemyData.DeathEffect, enemyData.EffectOffset, enemyHealth);
        }

        public GameObject CreateItem(CurrencyItemType type, Vector3 position)
        {
            CurrencyItemStaticData itemData = _staticData.GetItemStaticData(type);

            return Object.Instantiate(itemData.Prefab, position, Quaternion.identity);
        }

        public GameObject CreateItem(QuestItemType type, Vector3 position)
        {
            QuestItemStaticData itemData = _staticData.GetItemStaticData(type);

            return Object.Instantiate(itemData.Prefab, position, Quaternion.identity);
        }

        public void CreatePreyResource(PreyResourceType type, Vector3 position, Quaternion rotation, Transform parent)
        {
            PreyResourceStaticData preyResourceData = _staticData.GetPreyResourceStaticData(type);

            var preyResourceObject = Object.Instantiate(preyResourceData.Prefab, position, rotation, parent);

            var preyResource = preyResourceObject.GetComponent<PreyResource>();
            preyResource.Init(preyResourceData.HitsCountToDie, preyResourceData.DroppetItem);

            preyResourceObject.GetComponent<DamageableDeath>()
                .Init(preyResourceData.DiedEffect, preyResourceData.EffectOffset, preyResource);
        }

        public GameObject CreateTile(BiomeType type, Vector3 position, Quaternion rotation, Transform parent)
        {
            BiomeStaticData biomeData = _staticData.GetBiomeStaticData(type);

            GameObject tile = Object.Instantiate(biomeData.Prefab, position, rotation, parent);

            tile.GetComponent<PreyResourcesSpawner>()?
                .Init(this, biomeData.PreyResourcesTemplates, biomeData.PercentageFilled);

            tile.GetComponent<Biome>()
                .Init(type);

            return tile;
        }

        private float GetEnemyWeight(EnemyStaticData enemyData) =>
            (1 / enemyData.AttackCooldown) * enemyData.Damage + (enemyData.Health / 3);
    }
}