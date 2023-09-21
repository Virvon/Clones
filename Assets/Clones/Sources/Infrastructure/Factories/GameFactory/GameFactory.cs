using Cinemachine;
using Clones.Animation;
using Clones.StaticData;
using UnityEngine;
using Clones.Services;
using Clones.GameLogic;
using System;
using Object = UnityEngine.Object;

namespace Clones.Infrastructure
{
    public class GameFactory : IGameFacotry
    {
        private GameObject _playerObject;

        private readonly IStaticDataService _staticData;
        private readonly IAssetProvider _assets;
        private readonly IInputService _inputService;

        public event Action<IDroppable> DroppableCreated;

        public GameFactory(IAssetProvider assets, IInputService inputService, IStaticDataService staticData)
        {
            _assets = assets;
            _inputService = inputService;
            _staticData = staticData;
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
            worldGenerator.Init(_playerObject.transform, worldGeneratorData.GenerationBiomes, worldGeneratorData.ViewRadius, worldGeneratorData.CellSize);

            return worldGenerator;
        }

        public void CreateVirtualCamera()
        {
            _assets.Instantiate(AssetPath.VirtualCamera)
                .GetComponent<CinemachineVirtualCamera>()
                .Follow = _playerObject.transform;
        }

        public EnemiesSpawner CreateEnemiesSpawner(ICurrentBiome currentBiome)
        {
            GameObject enemiesSpawnerObject = _assets.Instantiate(AssetPath.EnemiesSpawner);

            EnemiesSpawner enemiesSpawner = enemiesSpawnerObject.GetComponent<EnemiesSpawner>();

            enemiesSpawner.Init(currentBiome, _staticData, _playerObject);

            return enemiesSpawner;
        }
    }
}