using Cinemachine;
using Clones.StaticData;
using UnityEngine;
using Clones.Services;
using Clones.GameLogic;
using Object = UnityEngine.Object;
using Clones.Types;
using UnityEngine.AI;

namespace Clones.Infrastructure
{
    public class GameFactory : IGameFacotry
    {
        private readonly IGameStaticDataService _gameStaticDataService;
        private readonly IAssetProvider _assets;
        private readonly ITimeScaler _timeScale;

        private EnemiesSpawner _enemiesSpawner;

        public GameFactory(IAssetProvider assets, IGameStaticDataService gameStaticDataService, ITimeScaler timeScale)
        {
            _assets = assets;
            _gameStaticDataService = gameStaticDataService;
            _timeScale = timeScale;
        }

        public WorldGenerator CreateWorldGenerator(GameObject player, IPartsFactory partsFactory)
        {
            WorldGeneratorStaticData worldGeneratorData = _gameStaticDataService.GetWorldGenerator();

            WorldGenerator worldGenerator = Object.Instantiate(worldGeneratorData.Prefab);
            worldGenerator.Init(partsFactory, player.transform, worldGeneratorData.GenerationBiomes, worldGeneratorData.ViewRadius, worldGeneratorData.DestroyRadius, worldGeneratorData.CellSize);

            NavMeshZone navMeshZone = Object.Instantiate(worldGeneratorData.NavMeshZonePrefab, worldGenerator.transform);
            navMeshZone.Init(worldGenerator.GetComponent<NavMeshSurface>(), player.transform);

            return worldGenerator;
        }

        public CinemachineVirtualCamera CreateVirtualCamera(GameObject player)
        {
            GameObject cameraObject = _assets.Instantiate(AssetPath.VirtualCamera);

            var virtualCamera = cameraObject.GetComponent<CinemachineVirtualCamera>();
            virtualCamera.Follow = player.transform;

            return virtualCamera;
        }

        public EnemiesSpawner CreateEnemiesSpawner(ICurrentBiome currentBiome, Complexity complexity, GameObject player, IPartsFactory partsFactory)
        {
            EnemiesSpawnerStaticData data = _gameStaticDataService.GetEnemiesSpawner(); 
            GameObject enemiesSpawnerObject = InstantiateRegistered(data.Prefab);

            _enemiesSpawner = enemiesSpawnerObject.GetComponent<EnemiesSpawner>();

            _enemiesSpawner.Init(data.StartDelay, data.SpawnCooldown, data.WaveWeight, data.MinRadius, data.MaxRadius, currentBiome, _gameStaticDataService, player, complexity, partsFactory);

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