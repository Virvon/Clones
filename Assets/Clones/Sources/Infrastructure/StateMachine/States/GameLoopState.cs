using Cinemachine;
using Clones.Data;
using Clones.GameLogic;
using Clones.Services;
using Clones.StaticData;
using Clones.UI;
using System.Collections.Generic;
using UnityEngine;

namespace Clones.Infrastructure
{
    public class GameLoopState : IState
    {
        private readonly IGameFacotry _gameFactory;
        private readonly IUiFactory _uiFactory;
        private readonly IPartsFactory _partsFactory;
        private readonly IPersistentProgressService _persistentProgress;
        private readonly ITimeScaler _timeScale;
        private readonly IMainMenuStaticDataService _mainMenuStaticDataService;
        private readonly ISaveLoadService _saveLoadService;
        private readonly IGameStaticDataService _gameStaticDataService;
        private readonly ICoroutineRunner _coroutineRunner;
        private readonly IAdvertisingDisplay _advertisingDisplay;
        private readonly ILocalization _localization;
        private readonly ICharacterFactory _characterFactory;

        private List<IDisable> _disables;
        private GameTimer _gameTimer;
        private GameObject _playerObject;
        private CharacterAttack _playerAttack;
        private IKiller _killer;

        private IPlayerRevival _playerRevival;
        private CurrentBiome _currentBiome;

        public GameLoopState(IGameFacotry gameFactory, IUiFactory uiFacotry, IPartsFactory partsFactory, IPersistentProgressService persistentProgress, ITimeScaler timeScale, IMainMenuStaticDataService mainMenuStaticDataService, ISaveLoadService saveLoadService, IGameStaticDataService gameStaticDataService, ICoroutineRunner coroutineRunner, IAdvertisingDisplay advertisingDisplay, ILocalization localization, ICharacterFactory characterFactory)
        {
            _gameFactory = gameFactory;
            _uiFactory = uiFacotry;
            _partsFactory = partsFactory;
            _persistentProgress = persistentProgress;
            _timeScale = timeScale;
            _mainMenuStaticDataService = mainMenuStaticDataService;
            _gameStaticDataService = gameStaticDataService;
            _saveLoadService = saveLoadService;
            _coroutineRunner = coroutineRunner;

            _disables = new();
            _advertisingDisplay = advertisingDisplay;
            _localization = localization;
            _characterFactory = characterFactory;
        }

        public void Enter() =>
            CreateGame();

        public void Exit()
        {
            Debug.Log("plaing time " + _gameTimer.LastMeasurement);
            _persistentProgress.Progress.AveragePlayTime.Add((int)_gameTimer.LastMeasurement);

            foreach (var disable in _disables)
                disable.OnDisable();

            UseSelectedClone();
            _saveLoadService.SaveProgress();
        }

        private void CreateGame()
        {
            float resourcesMultiplier = GetResourcesMultiplier();

            Complexity complexity = new Complexity(_persistentProgress, _gameStaticDataService.GetComplextiy().TargetPlayTime, _persistentProgress.Progress.AvailableClones.GetSelectedCloneData().Level);

            IQuestsCreator questsCreator = CreateQuestsCreator(resourcesMultiplier, complexity);

            CreatePlayer(questsCreator, resourcesMultiplier);
            CreateWorld();
            CreateDroppers(questsCreator);
            GameObject hud = CreateHud(questsCreator);
            CreateCamera();

            EnemiesSpawner enemiesSpawner = _gameFactory.CreateEnemiesSpawner(_currentBiome, complexity, _playerObject, _partsFactory);

            _uiFactory.CreateGameSettings(enemiesSpawner, _playerObject.GetComponent<PlayerHealth>());

            _gameFactory.CreateMusic(_currentBiome);
            _gameFactory.CreateFreezingScreen(_playerObject);

            CreateGameTimer();
            CreatePlayerDeath(hud, enemiesSpawner);

            questsCreator.Create();
            enemiesSpawner.StartSpawn();
            _gameTimer.Start();
        }

        private void CreatePlayerDeath(GameObject hud, EnemiesSpawner enemiesSpawner)
        {
            PlayerDeath playerDeath = new(hud.GetComponentInChildren<GameRevivalView>(), _playerObject.GetComponent<PlayerHealth>(), _timeScale, enemiesSpawner, callback: () => _gameTimer.Stop());
            _disables.Add(playerDeath);
        }

        private void CreateWorld()
        {
            WorldGenerator worldGenerator = _gameFactory.CreateWorldGenerator(_playerObject, _partsFactory);
            _currentBiome = new(worldGenerator);
            _disables.Add(_currentBiome);
        }

        private void CreateGameTimer()
        {
            _gameTimer = new GameTimer();
            _gameTimer.Init(_coroutineRunner);
            _timeScale.Add(_gameTimer);
        }

        private void CreateCamera()
        {
            CinemachineVirtualCamera virtualCamera = _gameFactory.CreateVirtualCamera(_playerObject);
            AttackShake attackShake = new(_playerAttack, virtualCamera.GetComponent<CameraShake>());
            _disables.Add(attackShake);
        }

        private GameObject CreateHud(IQuestsCreator questsCreator)
        {
            GameObject hud = _uiFactory.CreateHud(questsCreator, _playerObject);
            _uiFactory.CreateControl(_playerObject.GetComponent<Player>());
            _uiFactory.CreateGameOverView();
            _uiFactory.CreateGameRevivleView(_playerRevival);
            return hud;
        }

        private void CreateDroppers(IQuestsCreator questsCreator)
        {
            QuestItemsDropper questItemsDropper = new(_partsFactory, _killer, questsCreator);
            CurrencyDropper currencyDropper = new(_partsFactory, _killer);
            _disables.Add(questItemsDropper);
            _disables.Add(currencyDropper);
        }

        private void CreatePlayer(IQuestsCreator questsCreator, float resourcesMultiplier)
        {
            IItemsCounter itemsCounter = CreateItemsCounter(questsCreator, resourcesMultiplier);
            _playerObject = _characterFactory.CreateCharacter(_partsFactory, itemsCounter);
            _playerAttack = _playerObject.GetComponent<CharacterAttack>();
            _killer = _playerObject.GetComponent<IKiller>();
            _characterFactory.CreateWand(_playerObject.GetComponent<WandBone>().Bone);
            _playerRevival = new GamePlayerRevival(_playerObject.GetComponent<PlayerHealth>(), _advertisingDisplay);
        }

        private IQuestsCreator CreateQuestsCreator(float resourcesMultiplier, Complexity complexity)
        {
            QuestStaticData questStaticData = _gameStaticDataService.GetQuest();
            IQuestsCreator questsCreator = new QuestsCreator(_persistentProgress, questStaticData.QuestItemTypes, complexity, resourcesMultiplier, questStaticData.ItemsCount, questStaticData.MinItemsCountPercentInQuest, questStaticData.Reward, _gameStaticDataService, _localization);
            return questsCreator;
        }

        private float GetResourcesMultiplier()
        {
            WandData wandData = _persistentProgress.Progress.AvailableWands.GetSelectedWandData();
            CloneData cloneData = _persistentProgress.Progress.AvailableClones.GetSelectedCloneData();
            float resourcesMultiplier = cloneData.ResourceMultiplier * (1 + wandData.WandStats.PreyResourcesIncreasePercentage / 100f);
            return resourcesMultiplier;
        }

        private IItemsCounter CreateItemsCounter(IQuestsCreator questsCreator, float resourcesMultiplier)
        {
            ItemsCounterStaticData itemsCounterStaticData = _gameStaticDataService.GetItemsCounter();
            int DNAReward = (int)(itemsCounterStaticData.DNAReward * resourcesMultiplier);
            int questItemReward = (int)(itemsCounterStaticData.CollectingItemReward * resourcesMultiplier);

            return new ItemsCounter(questsCreator, _persistentProgress, DNAReward, questItemReward);
        }

        private void UseSelectedClone() => 
            _persistentProgress.Progress.AvailableClones.GetSelectedCloneData().Use(_mainMenuStaticDataService.GetClone(_persistentProgress.Progress.AvailableClones.SelectedClone).DisuseTime);
    }
}