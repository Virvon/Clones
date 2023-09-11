using Cinemachine;
using Clones.Animation;
using Clones.StaticData;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Clones.Infrastructure
{
    public class GameFactory : IGameFactory
    {
        private GameObject _playerObject;

        private readonly IStaticDataService _staticData;
        private readonly IAssetProvider _assets;
        private readonly IInputService _inputService;

        public GameFactory(IAssetProvider assets, IInputService inputService, IStaticDataService staticData)
        {
            _assets = assets;
            _inputService = inputService;
            _staticData = staticData;
        }

        public void CreateHud()
        {
            var hud = _assets.Instantiate(AssetPath.HudPath);

            hud.GetComponentInChildren<Freezbar>()
                .Init(_playerObject.GetComponentInChildren<PlayerFreezing>());

            hud.GetComponentInChildren<PlayerHealthbar>()
                .Init(_playerObject.GetComponent<PlayerHealth>());
        }

        public GameObject CreatePlayer()
        {
            _playerObject = _assets.Instantiate(AssetPath.PlayerPath);

            _playerObject.GetComponent<PlayerAnimationSwitcher>()
                .Init(_inputService);

            return _playerObject;
        }

        public void CreateWorldGenerator()
        {
            WorldGeneratorStaticData worldGeneratorData = _staticData.GetWorldGeneratorData();

            WorldGenerator worldGenerator = Object.Instantiate(worldGeneratorData.Prefab);
            worldGenerator.Init(this, _playerObject.transform, worldGeneratorData.GenerationBiomes, worldGeneratorData.ViewRadius, worldGeneratorData.CellSize);
        }

        public GameObject CreateTile(BiomeType type, Vector3 position, Quaternion rotation, Transform parent)
        {
            BiomeStaticData biomeData = _staticData.GetBiomeStaticData(type);

            var tile = Object.Instantiate(biomeData.Prefab, position, rotation, parent);

            tile.GetComponent<PreyResourcesSpawner>()?
                .Init(this, biomeData.PreyResourcesTamplates, biomeData.PercentageFilled);

            return tile;
        }

        public void CreateVirtualCamera()
        {
            _assets.Instantiate(AssetPath.VirtualCameraPath)
                .GetComponent<CinemachineVirtualCamera>()
                .Follow = _playerObject.transform;
        }

        public void CreatePreyResource(PreyResourceType type, Vector3 position, Quaternion rotation, Transform parent)
        {
            PreyResourceStaticData preyResourceData = _staticData.GetPreyResourceStaticData(type);

            var preyResource = Object.Instantiate(preyResourceData.Prefab, position, rotation, parent);
        }
    }
}