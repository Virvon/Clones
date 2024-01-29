using Cinemachine;
using Clones.Animation;
using Clones.StaticData;
using UnityEngine;
using Clones.Services;
using Clones.GameLogic;
using Object = UnityEngine.Object;
using Clones.Data;
using Clones.StateMachine;
using Clones.Audio;
using Clones.Types;

namespace Clones.Infrastructure
{
    public class GameFactory : IGameFacotry
    {
        private readonly IGameStaticDataService _gameStaticDataService;
        private readonly IAssetProvider _assets;
        private readonly IInputService _inputService;
        private readonly ITimeScale _timeScale;
        private readonly IPersistentProgressService _persistentPorgress;
        private readonly IMainMenuStaticDataService _mainMenuStaticDataService;

        private GameObject _playerObject;
        private EnemiesSpawner _enemiesSpawner;

        public GameFactory(IAssetProvider assets, IInputService inputService, IGameStaticDataService gameStaticDataService, ITimeScale timeScale, IPersistentProgressService persistentProgress, IMainMenuStaticDataService mainMenuStaticDataService)
        {
            _assets = assets;
            _inputService = inputService;
            _gameStaticDataService = gameStaticDataService;
            _timeScale = timeScale;
            _persistentPorgress = persistentProgress;
            _mainMenuStaticDataService = mainMenuStaticDataService;
        }

        public GameObject CreatePlayer(IPartsFactory partsFactory, IItemsCounter itemsCounter)
        {
            CloneData cloneData = _persistentPorgress.Progress.AvailableClones.GetSelectedCloneData();
            WandData wandData = _persistentPorgress.Progress.AvailableWands.GetSelectedWandData();
            CloneStaticData cloneStaticData = _mainMenuStaticDataService.GetClone(_persistentPorgress.Progress.AvailableClones.SelectedClone);
            WandStaticData wandStaticData = _mainMenuStaticDataService.GetWand(_persistentPorgress.Progress.AvailableWands.SelectedWand);

            int health = cloneData.Health + (int)(cloneData.Health * wandData.WandStats.HealthIncreasePercentage / 100f);
            int damage = cloneData.Damage + (int)(cloneData.Damage * wandData.WandStats.DamageIncreasePercentage / 100f);
            //int attackCooldown = cloneData.AttackCooldown - cloneData
            float resourceMultiplier = cloneData.ResourceMultiplier + cloneData.ResourceMultiplier * wandData.WandStats.PreyResourcesIncreasePercentage / 100f;

            _playerObject = Object.Instantiate(cloneStaticData.Prefab);

            Player player = _playerObject.GetComponent<Player>();

            player
                .GetComponent<Player>()
                .Init(cloneStaticData.MovementSpeed, cloneStaticData.AttackCooldown);

            _playerObject
                .GetComponent<PlayerAnimationSwitcher>()
                .Init(_inputService, player);

            _playerObject
                .GetComponent<DropCollecting>()
                .Init(itemsCounter, cloneStaticData.DropCollectingRadius);

            _playerObject
                .GetComponent<PlayerHealth>()
                .Init(health);

            _playerObject
                .GetComponent<PlayerStateMashine>()
                .Init(_inputService);

            _playerObject
                .GetComponent<MovementState>()
                .Init(_inputService, player, cloneStaticData.RotationSpeed);

            _playerObject
                .GetComponent<MiningState>()
                .Init(cloneStaticData.MiningRadius, cloneStaticData.RotationSpeed);

            _playerObject
                .GetComponent<EnemiesAttackState>()
                .Init(cloneStaticData.AttackRadius, cloneStaticData.RotationSpeed);

            _playerObject
                .GetComponent<Wand>()
                .Init(partsFactory, wandStaticData.Bullet, damage, wandStaticData.KnockbackForse, wandStaticData.KnockbackOffset, player);

            CreateWand(_playerObject.GetComponent<WandBone>().Bone);

            return _playerObject;
        }

        public WorldGenerator CreateWorldGenerator()
        {
            WorldGeneratorStaticData worldGeneratorData = _gameStaticDataService.GetWorldGenerator();

            WorldGenerator worldGenerator = Object.Instantiate(worldGeneratorData.Prefab);
            worldGenerator.Init(_playerObject.transform, worldGeneratorData.GenerationBiomes, worldGeneratorData.ViewRadius, worldGeneratorData.DestroyRadius, worldGeneratorData.CellSize);

            return worldGenerator;
        }

        public CinemachineVirtualCamera CreateVirtualCamera()
        {
            GameObject cameraObject = _assets.Instantiate(AssetPath.VirtualCamera);

            var virtualCamera = cameraObject.GetComponent<CinemachineVirtualCamera>();
            virtualCamera.Follow = _playerObject.transform;

            return virtualCamera;
        }

        public EnemiesSpawner CreateEnemiesSpawner(ICurrentBiome currentBiome, Complexity complexity)
        {
            EnemiesSpawnerStaticData data = _gameStaticDataService.GetEnemiesSpawner(); 
            GameObject enemiesSpawnerObject = InstantiateRegistered(data.Prefab);

            _enemiesSpawner = enemiesSpawnerObject.GetComponent<EnemiesSpawner>();

            _enemiesSpawner.Init(data.StartDelay, data.SpawnCooldown, data.WaveWeight, data.MinRadius, data.MaxRadius);
            _enemiesSpawner.Init(currentBiome, _gameStaticDataService, _playerObject, complexity);

            return _enemiesSpawner;
        }

        public GameMusic CreateMusic(ICurrentBiome currentBiome)
        {
            GameObject gameMusicObject = _assets.Instantiate(AssetPath.GameMusic);
            GameMusic gameMusic = gameMusicObject.GetComponent<GameMusic>();

            gameMusic.Init(currentBiome, _enemiesSpawner);

            foreach (BiomeType biomeType in _gameStaticDataService.GetWorldGenerator().GenerationBiomes)
                gameMusic.Add(biomeType, _gameStaticDataService.GetBiome(biomeType).CombatAudioSourcePrefab);

            return gameMusic;
        }

        public void CreateFreezingScreen()
        {
            GameObject freezingScreenObject = _assets.Instantiate(AssetPath.FreezingScreen, Camera.main.transform);
            FreezingScreen freezingScreen = freezingScreenObject.GetComponent<FreezingScreen>();
            CameraShader cameraShader = Camera.main.GetComponent<CameraShader>();

            freezingScreen.Init(cameraShader);

            _playerObject
                .GetComponentInChildren<FreezingScreenReporter>()
                .Init(freezingScreen);
        }

        private void CreateWand(Transform bone)
        {
            WandStaticData wandStaticData = _mainMenuStaticDataService.GetWand(_persistentPorgress.Progress.AvailableWands.SelectedWand);
            Object.Instantiate(wandStaticData.Prefab, bone);
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