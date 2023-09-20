using Cinemachine;
using Clones.Animation;
using Clones.StaticData;
using UnityEngine;
using Clones.Services;
using Clones.UI;
using Clones.GameLogic;
using System;
using Object = UnityEngine.Object;
using Clones.Biomes;
using UnityEngine.AI;
using Random = UnityEngine.Random;

namespace Clones.Infrastructure
{
    public class GameFactory : IGameFactory
    {
        private GameObject _playerObject;

        private readonly IStaticDataService _staticData;
        private readonly IAssetProvider _assets;
        private readonly IInputService _inputService;
        private readonly IPersistentProgressService _persistentProgressService;

        public event Action<IDroppable> DroppableCreated;

        public GameFactory(IAssetProvider assets, IInputService inputService, IStaticDataService staticData, IPersistentProgressService persistentProgressService)
        {
            _assets = assets;
            _inputService = inputService;
            _staticData = staticData;
            _persistentProgressService = persistentProgressService;
        }

        public void CreateHud(IQuestsCreator questsCreator)
        {
            var hud = _assets.Instantiate(AssetPath.Hud);

            hud.GetComponentInChildren<Freezbar>()
                .Init(_playerObject.GetComponentInChildren<PlayerFreezing>());

            hud.GetComponentInChildren<PlayerHealthbar>()
                .Init(_playerObject.GetComponent<PlayerHealth>());

            hud.GetComponentInChildren<QuestPanel>()
                .Init(questsCreator, this);

            hud.GetComponentInChildren<MoneyView>()
                .Init(_persistentProgressService.Progress.Wallet);

            hud.GetComponentInChildren<DnaView>()
                .Init(_persistentProgressService.Progress.Wallet);
        }

        public GameObject CreatePlayer(IItemsCounter itemsCounter)
        {
            _playerObject = _assets.Instantiate(AssetPath.Player);

            _playerObject.GetComponent<PlayerAnimationSwitcher>()
                .Init(_inputService);

            _playerObject.GetComponent<DropCollecting>()
                .Init(itemsCounter);

            return _playerObject;
        }

        public WorldGenerator CreateWorldGenerator()
        {
            WorldGeneratorStaticData worldGeneratorData = _staticData.GetWorldGeneratorData();

            WorldGenerator worldGenerator = Object.Instantiate(worldGeneratorData.Prefab);
            worldGenerator.Init(this, _playerObject.transform, worldGeneratorData.GenerationBiomes, worldGeneratorData.ViewRadius, worldGeneratorData.CellSize);

            return worldGenerator;
        }

        public GameObject CreateTile(BiomeType type, Vector3 position, Quaternion rotation, Transform parent)
        {
            BiomeStaticData biomeData = _staticData.GetBiomeStaticData(type);

            var tile = Object.Instantiate(biomeData.Prefab, position, rotation, parent);

            tile.GetComponent<PreyResourcesSpawner>()?
                .Init(this, biomeData.PreyResourcesTemplates, biomeData.PercentageFilled);

            tile.GetComponent<Biome>()
                .Init(type);

            return tile;
        }

        public void CreateVirtualCamera()
        {
            _assets.Instantiate(AssetPath.VirtualCamera)
                .GetComponent<CinemachineVirtualCamera>()
                .Follow = _playerObject.transform;
        }

        public void CreatePreyResource(PreyResourceType type, Vector3 position, Quaternion rotation, Transform parent)
        {
            PreyResourceStaticData preyResourceData = _staticData.GetPreyResourceStaticData(type);

            var preyResource = Object.Instantiate(preyResourceData.Prefab, position, rotation, parent);

            preyResource.GetComponent<PreyResource>()
                .Init(preyResourceData.HitsCountToDie, preyResourceData.DroppetItem);

            DroppableCreated?.Invoke(preyResource);
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

        public GameObject CreateQuestView(Quest quest, Transform parent)
        {
            var view = _assets.Instantiate(AssetPath.QuestView, parent);

            view.GetComponent<QuestView>()
                .Init(quest);

            return view;
        }

        public void CreateEnemy(EnemyType type, Vector3 position, Quaternion rotation, out float weight)
        {
            EnemyStaticData enemyData = _staticData.GetEnemyStaticData(type);

            weight = GetEnemyWeight(enemyData);

            var enemyObject = Object.Instantiate(enemyData.Prefab, position, rotation);

            enemyObject.GetComponent<Enemy>()
                .Init(_playerObject);

            enemyObject.GetComponent<NavMeshAgent>()
                .stoppingDistance = (float)Math.Round(Random.Range(enemyData.MinStopDistance, enemyData.MaxStopDistance), 2);

            enemyObject.GetComponent<MeleeAttack>()
                .Init(enemyData.Damage, enemyData.AttackCooldown);

            EnemyHealth enemyHealth = enemyObject.GetComponent<EnemyHealth>();

            enemyHealth.Init(enemyData.Health);

            enemyObject.GetComponentInChildren<EnemyHealthbar>()
                .Init(enemyHealth);
        }

        private float GetEnemyWeight(EnemyStaticData enemyData) => 
            (1 / enemyData.AttackCooldown) * enemyData.Damage + (enemyData.Health / 3);
    }
}