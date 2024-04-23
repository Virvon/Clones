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
        private readonly ITimeScale _timeScale;
        private readonly IMainMenuStaticDataService _mainMenuStaticDataService;
        private readonly ISaveLoadService _saveLoadService;
        private readonly IGameStaticDataService _gameStaticDataService;
        private readonly ICoroutineRunner _coroutineRunner;
        private readonly IAdvertisingDisplay _advertisingDisplay;
        private readonly ILocalization _localization;
        private readonly ICharacterFactory _characterFactory;

        private List<IDisable> _disables;
        private GameTimer _gameTimer;

        public GameLoopState(IGameFacotry gameFactory, IUiFactory uiFacotry, IPartsFactory partsFactory, IPersistentProgressService persistentProgress, ITimeScale timeScale, IMainMenuStaticDataService mainMenuStaticDataService, ISaveLoadService saveLoadService, IGameStaticDataService gameStaticDataService, ICoroutineRunner coroutineRunner, IAdvertisingDisplay advertisingDisplay, ILocalization localization, ICharacterFactory characterFactory)
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

        public void Enter()
        {
            CreateGame();
        }

        public void Exit()
        {
            _persistentProgress.Progress.AveragePlayTime.Add((int)_gameTimer.LastMeasurement);

            foreach (var disable in _disables)
                disable.OnDisable();

            UseSelectedClone();
            _saveLoadService.SaveProgress();

            Debug.Log("end game---------------");
            Debug.Log("plaing time " + _gameTimer.LastMeasurement);
        }

        private void CreateGame()
        {
            WandData wandData = _persistentProgress.Progress.AvailableWands.GetSelectedWandData();
            CloneData cloneData = _persistentProgress.Progress.AvailableClones.GetSelectedCloneData();
            float resourcesMultiplier = cloneData.ResourceMultiplier * (1 + wandData.WandStats.PreyResourcesIncreasePercentage / 100f);
            Complexity complexity = new Complexity(_persistentProgress, _gameStaticDataService.GetComplextiy().TargetPlayTime, _persistentProgress.Progress.AvailableClones.GetSelectedCloneData().Level);
            QuestStaticData questStaticData = _gameStaticDataService.GetQuest();
            IQuestsCreator questsCreator = new QuestsCreator(_persistentProgress, questStaticData.QuestItemTypes, complexity, resourcesMultiplier, questStaticData.ItemsCount, questStaticData.MinItemsCountPercentInQuest, questStaticData.Reward, _gameStaticDataService, _localization);
            IItemsCounter itemsCounter = CreateItemsCounter(questsCreator, resourcesMultiplier);

            GameObject playerObject = _characterFactory.CreateCharacter(_partsFactory, itemsCounter);
            CharacterAttack playerAttack = playerObject.GetComponent<CharacterAttack>();

            _characterFactory.CreateWand(playerObject.GetComponent<WandBone>().Bone);

            WorldGenerator worldGenerator = _gameFactory.CreateWorldGenerator(playerObject);
            worldGenerator.Init(_partsFactory);

            QuestItemsDropper questItemsDropper = new(_partsFactory, playerAttack, questsCreator);
            GamePlayerRevival playerRevival = new(playerObject.GetComponent<PlayerHealth>(), _advertisingDisplay);

            GameObject hud = _uiFactory.CreateHud(questsCreator, playerObject);
            _uiFactory.CreateControl(playerObject.GetComponent<Player>());
            _uiFactory.CreateGameOverView();
            _uiFactory.CreateGameRevivleView(playerRevival);

            CinemachineVirtualCamera virtualCamera = _gameFactory.CreateVirtualCamera(playerObject);
            AttackShake attackShake = new(playerAttack, virtualCamera.GetComponent<CameraShake>());
            CurrencyDropper currencyDropper = new(_partsFactory, playerAttack);
            ICurrentBiome currentBiome = new CurrentBiome(worldGenerator);

            EnemiesSpawner enemiesSpawner = _gameFactory.CreateEnemiesSpawner(currentBiome, complexity, playerObject);
            enemiesSpawner.Init(_partsFactory);

            _gameFactory.CreateMusic(currentBiome);
            _gameFactory.CreateFreezingScreen(playerObject);

            _gameTimer = new GameTimer();
            _gameTimer.Init(_coroutineRunner);
            

            PlayerDeath playerDeath = new(hud.GetComponentInChildren<GameRevivalView>(), playerObject.GetComponent<PlayerHealth>(), _timeScale, enemiesSpawner, callback: ()=> _gameTimer.Stop());

            hud.GetComponentInChildren<RevivalButton>()
                .Init(playerRevival);

            questsCreator.Create();
            enemiesSpawner.StartSpawn();
            _gameTimer.Start();

            _disables.Add(attackShake);
            _disables.Add(currentBiome);
            _disables.Add(playerDeath);
            _disables.Add(currencyDropper);
            _disables.Add(questItemsDropper);

            _timeScale.Add(_gameTimer);
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