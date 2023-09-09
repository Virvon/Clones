using Cinemachine;
using Clones.Animation;
using UnityEngine;

namespace Clones.Infrastructure
{
    public class GameFactory : IGameFactory
    {
        private GameObject _playerObject;

        private readonly IAssetProvider _assets;
        private readonly IInputService _inputService;

        public GameFactory(IAssetProvider assets, IInputService inputService)
        {
            _assets = assets;
            _inputService = inputService;
        }

        public void CreateHud()
        {
            var had = _assets.Instantiate(AssetPath.HudPath);

            had.GetComponentInChildren<Freezbar>()
                .Init(_playerObject.GetComponentInChildren<PlayerFreezing>());
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
            _assets.Instantiate(AssetPath.WorldGeneratorPath)
                .GetComponent<WorldGenerator2>()
                .Init(_playerObject.transform);
        }

        public void CreateVirtualCamera()
        {
            _assets.Instantiate(AssetPath.VirtualCameraPath)
                .GetComponent<CinemachineVirtualCamera>()
                .Follow = _playerObject.transform;
        }
    }
}