﻿using Cinemachine;
using Clones.Animation;
using Clones.StaticData;
using UnityEngine;
using Clones.Services;
using Clones.GameLogic;
using Object = UnityEngine.Object;
using Clones.Data;
using Clones.StateMachine;
using Clones.Types;
using Clones.SFX;

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
            CloneStaticData cloneStaticData = _mainMenuStaticDataService.GetClone(_persistentPorgress.Progress.AvailableClones.SelectedClone) ?? _mainMenuStaticDataService.GetClone(CloneType.Normal);
            WandStaticData wandStaticData = GetWandStaticData();

            int health;
            int damage;
            float attackCooldown;

            if (cloneData != null && wandData != null)
            {
                health = cloneData.Health + (int)(cloneData.Health * wandData.WandStats.HealthIncreasePercentage / 100f);
                damage = cloneData.Damage + (int)(cloneData.Damage * wandData.WandStats.DamageIncreasePercentage / 100f);
                attackCooldown = cloneData.AttackCooldown * (1 - wandData.WandStats.AttackCooldownDecreasePercentage / 100f);
            }
            else
            {
                health = cloneStaticData.Helath;
                damage = cloneStaticData.Damage;
                attackCooldown = cloneStaticData.AttackCooldown;
            }

            _playerObject = Object.Instantiate(cloneStaticData.Prefab);

            Player player = _playerObject.GetComponent<Player>();

            player
                .GetComponent<Player>()
                .Init(cloneStaticData.MovementSpeed, attackCooldown);


            PlayerAnimationSwitcher playerAnimationSwitcher = _playerObject.GetComponent<PlayerAnimationSwitcher>();

            if(wandData != null)
                playerAnimationSwitcher.Init(_inputService, player, wandData.WandStats.AttackCooldownDecreasePercentage);
            else
                playerAnimationSwitcher.Init(_inputService, player);

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

            _playerObject
                .GetComponentInChildren<MovementSound>()
                .Init(player);

            _playerObject
                .GetComponentInChildren<CharacterShootSound>()
                .Init(wandStaticData.ShootAudio, wandStaticData.ShootAudioVolume);

            _playerObject
                .GetComponent<PlayerRebornEffect>()
                .Init(cloneStaticData.RebornEffect, cloneStaticData.RebornEffectOffset);

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
            WandStaticData wandStaticData = GetWandStaticData();
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

        private WandStaticData GetWandStaticData() => 
            _mainMenuStaticDataService.GetWand(_persistentPorgress.Progress.AvailableWands.SelectedWand) ?? _mainMenuStaticDataService.GetWand(WandType.BranchWand);
    }
}