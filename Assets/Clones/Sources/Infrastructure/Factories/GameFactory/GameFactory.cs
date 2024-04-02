using Cinemachine;
using Clones.StaticData;
using UnityEngine;
using Clones.Services;
using Clones.GameLogic;
using Object = UnityEngine.Object;
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

        public WorldGenerator CreateWorldGenerator(GameObject player)
        {
            WorldGeneratorStaticData worldGeneratorData = _gameStaticDataService.GetWorldGenerator();

            WorldGenerator worldGenerator = Object.Instantiate(worldGeneratorData.Prefab);
            worldGenerator.Init(player.transform, worldGeneratorData.GenerationBiomes, worldGeneratorData.ViewRadius, worldGeneratorData.DestroyRadius, worldGeneratorData.CellSize);

            return worldGenerator;
        }

        public CinemachineVirtualCamera CreateVirtualCamera(GameObject player)
        {
            GameObject cameraObject = _assets.Instantiate(AssetPath.VirtualCamera);

            var virtualCamera = cameraObject.GetComponent<CinemachineVirtualCamera>();
            virtualCamera.Follow = player.transform;

            return virtualCamera;
        }

        public EnemiesSpawner CreateEnemiesSpawner(ICurrentBiome currentBiome, Complexity complexity, GameObject player)
        {
            EnemiesSpawnerStaticData data = _gameStaticDataService.GetEnemiesSpawner(); 
            GameObject enemiesSpawnerObject = InstantiateRegistered(data.Prefab);

            _enemiesSpawner = enemiesSpawnerObject.GetComponent<EnemiesSpawner>();

            _enemiesSpawner.Init(data.StartDelay, data.SpawnCooldown, data.WaveWeight, data.MinRadius, data.MaxRadius);
            _enemiesSpawner.Init(currentBiome, _gameStaticDataService, player, complexity);

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

        public void CreateFreezingScreen(GameObject player)
        {
            GameObject freezingScreenObject = _assets.Instantiate(AssetPath.FreezingScreen, Camera.main.transform);
            FreezingScreen freezingScreen = freezingScreenObject.GetComponent<FreezingScreen>();
            CameraShader cameraShader = Camera.main.GetComponent<CameraShader>();

            freezingScreen.Init(cameraShader);

            player
                .GetComponentInChildren<FreezingScreenReporter>()
                .Init(freezingScreen);
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