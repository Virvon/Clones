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

        private List<IDisable> _disables;

        public GameLoopState(GameStateMachine stateMachine, IGameFacotry gameFactory, IUiFactory uiFacotry, IPartsFactory partsFactory, IPersistentProgressService persistentProgress, ITimeScale timeScale)
        {
            _gameFactory = gameFactory;
            _uiFactory = uiFacotry;
            _partsFactory = partsFactory;
            _persistentProgress = persistentProgress;
            _timeScale = timeScale;

            _disables = new();
        }

        public void Enter() => 
            CreateGame();

        public void Exit()
        {
            foreach (var disable in _disables)
                disable.OnDisable();
        }

        private void CreateGame()
        {
            IQuestsCreator questsCreator = new QuestsCreator(_persistentProgress);

            IItemsCounter itemsCounter = new ItemsCounter(questsCreator, _persistentProgress);

            GameObject playerObject = _gameFactory.CreatePlayer(itemsCounter);

            CharacterAttack playerAttack = playerObject.GetComponent<CharacterAttack>();

            WorldGenerator worldGenerator = _gameFactory.CreateWorldGenerator();
            worldGenerator.Init(_partsFactory);

            GameObject hud = _uiFactory.CreateHud(questsCreator, playerObject);

            CinemachineVirtualCamera virtualCamera = _gameFactory.CreateVirtualCamera();

            AttackShake attackShake = new(playerAttack, virtualCamera.GetComponent<CameraShake>());

            CurrencyDropper currencyDropper = new(_partsFactory, playerAttack);
            QuestItemsDropper questItemsDropper = new(_partsFactory, playerAttack, questsCreator);

            ICurrentBiome currentBiome = new CurrentBiome(worldGenerator);

            EnemiesSpawner enemiesSpawner = _gameFactory.CreateEnemiesSpawner(currentBiome);
            enemiesSpawner.Init(_partsFactory);


            PlayerDeath playerDeath = new(hud.GetComponentInChildren<GameOverView>(), playerObject.GetComponent<PlayerHealth>(), _timeScale, enemiesSpawner);
            PlayerRevival playerRevival = new(playerObject.GetComponent<PlayerHealth>(), _timeScale);

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
    }
}