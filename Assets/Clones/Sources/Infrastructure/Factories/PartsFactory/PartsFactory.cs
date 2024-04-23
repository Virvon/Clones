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
using Clones.Data;
using Clones.SFX;

namespace Clones.Infrastructure
{
    public class PartsFactory : IPartsFactory
    {
        private readonly IGameStaticDataService _staticData;

        public PartsFactory(IGameStaticDataService staticData)
        {
            _staticData = staticData;
        }

        public void CreateEnemy(EnemyType type, Vector3 position, Quaternion rotation, Transform parent, out float weight, GameObject playerObject, float complexityCoefficient, int currentWave)
        {
            EnemyStaticData enemyData = _staticData.GetEnemy(type);

            float rootComplexityCoefficient = (float)Math.Sqrt(complexityCoefficient);

            weight = GetEnemyWeight(enemyData, complexityCoefficient);

            var enemyObject = Object.Instantiate(enemyData.Prefab, position, rotation, parent);

            enemyObject.GetComponent<Enemy>()
                .Init(playerObject);

            enemyObject.GetComponent<NavMeshAgent>()
                .stoppingDistance = (float)Math.Round(Random.Range(enemyData.MinStopDistance, enemyData.MaxStopDistance), 2);

            float speedIncrease = Mathf.Min(currentWave, enemyData.MaxWavesWithSpeedIncrease) * enemyData.SpeedIncreasePerWave;
            enemyObject.GetComponent<NavMeshAgent>().speed *= (1 + speedIncrease);

            MeleeAttack meleeAttack = enemyObject.GetComponentInChildren<MeleeAttack>();
            ShootingAttack shootingAttack = enemyObject.GetComponentInChildren<ShootingAttack>();

            if(meleeAttack != null)
                meleeAttack.Init(enemyData.Damage * rootComplexityCoefficient, enemyData.AttackCooldown);

            if(shootingAttack != null)
                shootingAttack.Init(this, ((ShootingEnemyStaticData)enemyData).BulletType, (int)enemyData.Damage);

            EnemyHealth enemyHealth = enemyObject.GetComponent<EnemyHealth>();
            enemyHealth.Init((int)(enemyData.Health * rootComplexityCoefficient));

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
            item
                .GetComponent<QuestItem>()
                .Init(type);

            return item;
        }

        public void CreatePreyResource(PreyResourceType type, Vector3 position, Quaternion rotation, Transform parent)
        {
            PreyResourceStaticData preyResourceData = _staticData.GetPreyResource(type);

            var preyResourceObject = Object.Instantiate(preyResourceData.Prefab, position, rotation, parent);

            var preyResource = preyResourceObject.GetComponent<PreyResource>();
            preyResource.Init(preyResourceData.HitsCountToDie, preyResourceData.DroppetItem);

            preyResourceObject
                .GetComponent<DamageableDeath>()
                .Init(preyResourceData.DiedEffect, preyResourceData.EffectOffset, preyResource);

            preyResourceObject
                .GetComponentInChildren<PreyResourceDamageSound>()
                .Init(preyResourceData.DamageAudio, preyResourceData.DamageAudioVolume);

            preyResourceObject
                .GetComponentInChildren<PreyResourceDieSound>()
                .Init(preyResourceData.DieAudio, preyResourceData.DieAudioVolume);
        }

        public void CreateUnminedResource(UnminedResourceType type, Vector3 position, Quaternion rotation, Transform parent)
        {
            UnminedPreyResourceStaticData unminedResourceData = _staticData.GetUnminedResource(type);

            Object.Instantiate(unminedResourceData.Prefab, position, rotation, parent);
        }

        public GameObject CreateTile(BiomeType type, Vector3 position, Quaternion rotation, Transform parent)
        {
            BiomeStaticData biomeData = _staticData.GetBiome(type);

            GameObject tile = Object.Instantiate(biomeData.GetPrefab(), position, rotation, parent);

            tile.GetComponent<PreyResourcesSpawner>()?.Init(this, biomeData.PreyResourcesTypes, biomeData.PreyResourcesPercentageFilled);

            tile.GetComponent<UnminedResourcesSpawner>()?.Init(this, biomeData.UnminedResourcesTypes, biomeData.UnminedResourcesPercentageFilled);

            tile.GetComponentInChildren<Biome>()
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

        private float GetEnemyWeight(EnemyStaticData enemyData, float complexityCoefficient) =>
            ((enemyData.Damage * enemyData.Health) / enemyData.AttackCooldown) * complexityCoefficient;
    }
}