using Cinemachine;
using Clones.Animation;
using Clones.StaticData;
using UnityEngine;
using Clones.Services;
using Clones.GameLogic;
using Object = UnityEngine.Object;
using Clones.Data;
using Clones.StateMachine;

namespace Clones.Infrastructure
{
    public class GameFactory : IGameFacotry
    {
        private readonly IGameStaticDataService _staticData;
        private readonly IAssetProvider _assets;
        private readonly IInputService _inputService;
        private readonly ITimeScale _timeScale;
        private readonly IPersistentProgressService _persistentPorgress;
        private readonly IMainMenuStaticDataService _mainMenuStaticDataService;

        private GameObject _playerObject;

        public GameFactory(IAssetProvider assets, IInputService inputService, IGameStaticDataService staticData, ITimeScale timeScale, IPersistentProgressService persistentProgress, IMainMenuStaticDataService mainMenuStaticDataService)
        {
            _assets = assets;
            _inputService = inputService;
            _staticData = staticData;
            _timeScale = timeScale;
            _persistentPorgress = persistentProgress;
            _mainMenuStaticDataService = mainMenuStaticDataService;
        }

        public GameObject CreatePlayer(IItemsCounter itemsCounter)
        {
            CloneData cloneData = _persistentPorgress.Progress.AvailableClones.GetSelectedCloneData();
            CloneStaticData cloneStaticData = _mainMenuStaticDataService.GetClone(_persistentPorgress.Progress.AvailableClones.SelectedClone);

            _playerObject = Object.Instantiate(cloneStaticData.Prefab);

            _playerObject.GetComponent<PlayerAnimationSwitcher>()
                .Init(_inputService);

            _playerObject.GetComponent<DropCollecting>()
                .Init(itemsCounter);

            _playerObject.GetComponent<PlayerHealth>()
                .Init(cloneData.Health);

            _playerObject.GetComponent<MovementState>()
                .Init(cloneStaticData.MovementSpeed, cloneStaticData.RotationSpeed);

            return _playerObject;
        }

        public WorldGenerator CreateWorldGenerator()
        {
            WorldGeneratorStaticData worldGeneratorData = _staticData.GetWorldGeneratorData();

            WorldGenerator worldGenerator = Object.Instantiate(worldGeneratorData.Prefab);
            worldGenerator.Init(_playerObject.transform, worldGeneratorData.GenerationBiomes, worldGeneratorData.ViewRadius, worldGeneratorData.CellSize);

            return worldGenerator;
        }

        public CinemachineVirtualCamera CreateVirtualCamera()
        {
            GameObject cameraObject = _assets.Instantiate(AssetPath.VirtualCamera);

            var virtualCamera = cameraObject.GetComponent<CinemachineVirtualCamera>();
            virtualCamera.Follow = _playerObject.transform;

            return virtualCamera;
        }

        public EnemiesSpawner CreateEnemiesSpawner(ICurrentBiome currentBiome)
        {
            GameObject enemiesSpawnerObject = InstantiateRegistered(AssetPath.EnemiesSpawner);

            EnemiesSpawner enemiesSpawner = enemiesSpawnerObject.GetComponent<EnemiesSpawner>();

            enemiesSpawner.Init(currentBiome, _staticData, _playerObject);

            return enemiesSpawner;
        }

        private GameObject InstantiateRegistered(string prefabPath)
        {
            GameObject gameObject = _assets.Instantiate(prefabPath);

            RegisterTimeScalables(gameObject);

            return gameObject;
        }

        private GameObject InstantiateRegistered(GameObject prefab)
        {
            GameObject gameObject = Object.Instantiate(prefab);

            RegisterTimeScalables(gameObject);

            return gameObject;
        }

        private void RegisterTimeScalables(GameObject gameObject)
        {
            foreach(ITimeScalable timeScalable in gameObject.GetComponentsInChildren<ITimeScalable>())
                _timeScale.Add(timeScalable);
        }
    }
}