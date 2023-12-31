﻿using Clones.StaticData;
using UnityEngine;
using Clones.Services;
using Clones.GameLogic;
using System;
using Object = UnityEngine.Object;
using Clones.Biomes;
using UnityEngine.AI;
using Random = UnityEngine.Random;
using Clones.Types;
using Clones.Data;

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
            EnemyStaticData enemyData = _staticData.GetEnemy(type);

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
            CurrencyItemStaticData itemData = _staticData.GetItem(type);

            return Object.Instantiate(itemData.Prefab, position, Quaternion.identity);
        }

        public GameObject CreateItem(QuestItemType type, Vector3 position)
        {
            QuestItemStaticData itemData = _staticData.GetItem(type);

            GameObject item = Object.Instantiate(itemData.Prefab, position, Quaternion.identity);
            item.GetComponent<QuestItem>().Init(type);

            return item;
        }

        public void CreatePreyResource(PreyResourceType type, Vector3 position, Quaternion rotation, Transform parent)
        {
            PreyResourceStaticData preyResourceData = _staticData.GetPreyResource(type);

            var preyResourceObject = Object.Instantiate(preyResourceData.Prefab, position, rotation, parent);

            var preyResource = preyResourceObject.GetComponent<PreyResource>();
            preyResource.Init(preyResourceData.HitsCountToDie, preyResourceData.DroppetItem);

            preyResourceObject.GetComponent<DamageableDeath>()
                .Init(preyResourceData.DiedEffect, preyResourceData.EffectOffset, preyResource);
        }

        public void CreateUnminedResource(UnminedResourceType type, Vector3 position, Quaternion rotation, Transform parent)
        {
            UnminedPreyResourceStaticData unminedResourceData = _staticData.GetUnminedResource(type);

            Object.Instantiate(unminedResourceData.Prefab, position, rotation, parent);
        }

        public GameObject CreateTile(BiomeType type, Vector3 position, Quaternion rotation, Transform parent)
        {
            BiomeStaticData biomeData = _staticData.GetBiome(type);

            GameObject tile = Object.Instantiate(biomeData.Prefab, position, rotation, parent);

            tile.GetComponent<PreyResourcesSpawner>()?.Init(this, biomeData.PreyResourcesTypes, biomeData.PreyResourcesPercentageFilled);

            tile.GetComponent<UnminedResourcesSpawner>()?.Init(this, biomeData.UnminedResourcesTypes, biomeData.UnminedResourcesPercentageFilled);

            tile.GetComponent<Biome>()
                .Init(type);

            return tile;
        }

        public Bullet CreateBullet(BulletType type)
        {
            BulletStaticData bulletData = _staticData.GetBullet(type);
            GameObject bulletObject = Object.Instantiate(bulletData.BulletPrefab.gameObject);

            Bullet bullet = bulletObject.GetComponent<Bullet>();
            bullet.Init(bulletData);

            return bullet;
        }

        private float GetEnemyWeight(EnemyStaticData enemyData) =>
            (1 / enemyData.AttackCooldown) * enemyData.Damage + (enemyData.Health / 3);
    }
}