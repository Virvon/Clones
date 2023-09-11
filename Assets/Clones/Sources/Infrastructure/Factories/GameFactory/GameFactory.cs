using Cinemachine;
using Clones.Animation;
using Clones.Data;
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
            WorldGeneratorData worldGeneratorData = _staticData.GetWorldGeneratorData();
            WorldGenerator2 worldGenerator = Object.Instantiate(worldGeneratorData.Prefab);

            worldGenerator.Init(_playerObject.transform);
        }

        public void CreateVirtualCamera()
        {
            _assets.Instantiate(AssetPath.VirtualCameraPath)
                .GetComponent<CinemachineVirtualCamera>()
                .Follow = _playerObject.transform;
        }
    }
}