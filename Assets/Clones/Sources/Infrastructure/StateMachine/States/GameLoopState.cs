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

        private List<IDisable> _disables;
        private GameTimer _gameTimer;

        public GameLoopState(GameStateMachine stateMachine, IGameFacotry gameFactory, IUiFactory uiFacotry, IPartsFactory partsFactory, IPersistentProgressService persistentProgress, ITimeScale timeScale, IMainMenuStaticDataService mainMenuStaticDataService, ISaveLoadService saveLoadService, IGameStaticDataService gameStaticDataService)
        {
            _gameFactory = gameFactory;
            _uiFactory = uiFacotry;
            _partsFactory = partsFactory;
            _persistentProgress = persistentProgress;
            _timeScale = timeScale;
            _mainMenuStaticDataService = mainMenuStaticDataService;
            _gameStaticDataService = gameStaticDataService;

            _disables = new();
            _saveLoadService = saveLoadService;
        }

        public void Enter()
        {
            CreateGame();
        }

        public void Exit()
        {
            _persistentProgress.Progress.AveragePlayTime.Add(_gameTimer.LastMeasurement);

            foreach (var disable in _disables)
                disable.OnDisable();

            UseSelectedClone();
            _saveLoadService.SaveProgress();
        }

        private void CreateGame()
        {
            IQuestsCreator questsCreator = new QuestsCreator(_persistentProgress, _gameStaticDataService.GetQuest().QuestItemTypes);
            IItemsCounter itemsCounter = CreateItemsCounter(questsCreator);

            GameObject playerObject = _gameFactory.CreatePlayer(_partsFactory, itemsCounter);
            CharacterAttack playerAttack = playerObject.GetComponent<CharacterAttack>();

            WorldGenerator worldGenerator = _gameFactory.CreateWorldGenerator();
            worldGenerator.Init(_partsFactory);

            QuestItemsDropper questItemsDropper = new(_partsFactory, playerAttack, questsCreator);
            PlayerRevival playerRevival = new(playerObject.GetComponent<PlayerHealth>(), _timeScale);

            GameObject hud = _uiFactory.CreateHud(questsCreator, playerObject, playerRevival);
            _uiFactory.CreateControl(playerObject.GetComponent<Player>());

            CinemachineVirtualCamera virtualCamera = _gameFactory.CreateVirtualCamera();
            AttackShake attackShake = new(playerAttack, virtualCamera.GetComponent<CameraShake>());
            CurrencyDropper currencyDropper = new(_partsFactory, playerAttack);
            ICurrentBiome currentBiome = new CurrentBiome(worldGenerator);
            Complexity complexity = new Complexity(_persistentProgress, _gameStaticDataService.GetComplextiy().TargetPlayTime, _persistentProgress.Progress.AvailableClones.GetSelectedCloneData().Level);

            EnemiesSpawner enemiesSpawner = _gameFactory.CreateEnemiesSpawner(currentBiome, complexity);
            enemiesSpawner.Init(_partsFactory);

            _gameFactory.CreateMusic(currentBiome);
            _gameFactory.CreateFreezingScreen();

            _gameTimer = new GameTimer();
            _gameTimer.Start();

            PlayerDeath playerDeath = new(hud.GetComponentInChildren<RevivalView>(), playerObject.GetComponent<PlayerHealth>(), _timeScale, enemiesSpawner, _gameTimer);

            hud.GetComponentInChildren<RevivalButton>()
                .Init(playerRevival);

            questsCreator.Create();
            enemiesSpawner.StartSpawn();

            _disables.Add(attackShake);
            _disables.Add(currentBiome);
            _disables.Add(playerDeath);
            _disables.Add(currencyDropper);
            _disables.Add(questItemsDropper);
        }

        private IItemsCounter CreateItemsCounter(IQuestsCreator questsCreator)
        {
            ItemsCounterStaticData itemsCounterStaticData = _gameStaticDataService.GetItemsCounter();
            WandData wandData = _persistentProgress.Progress.AvailableWands.GetSelectedWandData();
            CloneData cloneData = _persistentProgress.Progress.AvailableClones.GetSelectedCloneData();
            float resourcesMultiplier = cloneData.ResourceMultiplier * (1 + wandData.WandStats.PreyResourcesIncreasePercentage / 100f);
            int DNAReward = (int)(itemsCounterStaticData.DNAReward * resourcesMultiplier);
            int questItemReward = (int)(itemsCounterStaticData.CollectingItemReward * resourcesMultiplier);

            return new ItemsCounter(questsCreator, _persistentProgress, DNAReward, questItemReward);
        }

        private void UseSelectedClone()
        {
            _persistentProgress.Progress.AvailableClones.GetSelectedCloneData().Use(_mainMenuStaticDataService.GetClone(_persistentProgress.Progress.AvailableClones.SelectedClone).DisuseTime);
        }
    }
}