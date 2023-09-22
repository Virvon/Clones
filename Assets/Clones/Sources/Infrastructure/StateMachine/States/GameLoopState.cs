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
        private readonly IStaticDataService _staticDataService;

        private IQuestsCreator _questsCreator;
        private IItemsCounter _itemsCounter;
        private ICurrentBiome _currentBiome;
        private GameObject _playerObject;
        private WorldGenerator _worldGenerator;
        private GameObject _hud;

        private List<IDisable> _disables;
        
        public GameLoopState(GameStateMachine stateMachine, IGameFacotry gameFactory, IUiFactory uiFacotry, IPartsFactory partsFactory, IPersistentProgressService persistentProgress, IStaticDataService staticDataService)
        {
            _gameFactory = gameFactory;
            _uiFactory = uiFacotry;
            _partsFactory = partsFactory;
            _persistentProgress = persistentProgress;
            _staticDataService = staticDataService;
        }

        public void Enter()
        {
            _disables = new();

            CreateGameInfrustructure();
            CreateGameWorld();

            CurrencyDropper currencyDropper = new(_partsFactory, _playerObject.GetComponent<CharacterAttack>());

            _questsCreator.Create();
            _currentBiome = new CurrentBiome(_worldGenerator);

            EnemiesSpawner enemiesSpawner = _gameFactory.CreateEnemiesSpawner(_currentBiome);

            enemiesSpawner.Init(_partsFactory);
            enemiesSpawner.StartSpawn();

            PlayerDeath playerDeath = new(_hud.GetComponentInChildren<GameOverView>(), _playerObject.GetComponent<PlayerHealth>());
            PlayerRevival playerRevival = new (_playerObject.GetComponent<PlayerHealth>());

            _hud.GetComponentInChildren<RevivalButton>()
                .Init(playerRevival);

            _disables.Add(_currentBiome);
            _disables.Add(playerDeath);
            _disables.Add(currencyDropper);
        }

        public void Exit()
        {
            foreach (var disable in _disables)
                disable.OnDisable();
        }

        private void CreateGameInfrustructure()
        {
            _questsCreator = new QuestsCreator(_persistentProgress);
            IDestroyDroppableReporter destroyDroppableReporter = new DestroyDroppableReporter(_partsFactory);
            _itemsCounter = new ItemsCounter(_questsCreator, _persistentProgress);
            
            QuestItemsDropper questItemsDropper = new(_partsFactory, destroyDroppableReporter, _questsCreator);

            _disables.Add(destroyDroppableReporter);
            _disables.Add(questItemsDropper);
        }

        private void CreateGameWorld()
        {
            _playerObject = _gameFactory.CreatePlayer(_itemsCounter);
            _worldGenerator = _gameFactory.CreateWorldGenerator();

            _worldGenerator.Init(_partsFactory);

            _hud = _uiFactory.CreateHud(_questsCreator, _playerObject);
            _gameFactory.CreateVirtualCamera();
        }
    }
}