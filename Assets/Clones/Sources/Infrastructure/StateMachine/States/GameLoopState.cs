using Cinemachine;
using Clones.GameLogic;
using Clones.Services;
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

        private List<IDisable> _disables;

        public GameLoopState(GameStateMachine stateMachine, IGameFacotry gameFactory, IUiFactory uiFacotry, IPartsFactory partsFactory, IPersistentProgressService persistentProgress, ITimeScale timeScale, IMainMenuStaticDataService mainMenuStaticDataService, ISaveLoadService saveLoadService)
        {
            _gameFactory = gameFactory;
            _uiFactory = uiFacotry;
            _partsFactory = partsFactory;
            _persistentProgress = persistentProgress;
            _timeScale = timeScale;
            _mainMenuStaticDataService = mainMenuStaticDataService;

            _disables = new();
            _saveLoadService = saveLoadService;
        }

        public void Enter()
        {
            CreateGame();
        }

        public void Exit()
        {
            foreach (var disable in _disables)
                disable.OnDisable();

            UseSelectedClone();
            _saveLoadService.SaveProgress();
        }

        private void CreateGame()
        {
            IQuestsCreator questsCreator = new QuestsCreator(_persistentProgress);

            IItemsCounter itemsCounter = new ItemsCounter(questsCreator, _persistentProgress);

            GameObject playerObject = _gameFactory.CreatePlayer(itemsCounter);

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

            EnemiesSpawner enemiesSpawner = _gameFactory.CreateEnemiesSpawner(currentBiome);
            enemiesSpawner.Init(_partsFactory);

            PlayerDeath playerDeath = new(hud.GetComponentInChildren<RevivalView>(), playerObject.GetComponent<PlayerHealth>(), _timeScale, enemiesSpawner);

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

        private void UseSelectedClone()
        {
            _persistentProgress.Progress.AvailableClones.GetSelectedCloneData().Use(_mainMenuStaticDataService.GetClone(_persistentProgress.Progress.AvailableClones.SelectedClone).DisuseTime);
        }
    }
}