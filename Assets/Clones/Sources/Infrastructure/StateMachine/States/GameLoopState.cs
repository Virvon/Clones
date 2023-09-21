using Clones.GameLogic;
using Clones.Services;
using Clones.UI;
using System.Collections.Generic;
using UnityEngine;

namespace Clones.Infrastructure
{
    public class GameLoopState : IState
    {
        private readonly IGameFactory _gameFactory;
        private readonly IPersistentProgressService _persistentProgress;
        private readonly ICoroutineRunner _coroutineRunner;
        private readonly IStaticDataService _staticDataService;

        private IQuestsCreator _questsCreator;
        private IItemsCounter _itemsCounter;
        private ICurrentBiome _currentBiome;
        private IEnemiesSpawner _enemiesSpawner;
        private GameObject _player;
        private WorldGenerator _worldGenerator;
        private GameObject _hud;

        private List<IDisable> _disables;

        public GameLoopState(GameStateMachine stateMachine, IGameFactory gameFactory, ICoroutineRunner coroutineRunner, IPersistentProgressService persistentProgress, IStaticDataService staticDataService)
        {
            _gameFactory = gameFactory;
            _coroutineRunner = coroutineRunner;
            _persistentProgress = persistentProgress;
            _staticDataService = staticDataService;
        }

        public void Enter()
        {
            _disables = new();

            CreateGameInfrustructure();
            CreateGameWorld();           

            _questsCreator.Create();
            _currentBiome = new CurrentBiome(_worldGenerator);

            CreateEnemiesSpawner();

            PlayerDeath playerDeath = new(_hud.GetComponentInChildren<GameOverView>(), _player.GetComponent<PlayerHealth>());
            PlayerRevival playerRevival = new (_player.GetComponent<PlayerHealth>());

            _hud.GetComponentInChildren<RevivalButton>()
                .Init(playerRevival);

            _disables.Add(_currentBiome);
            _disables.Add(playerDeath);
        }

        public void Exit()
        {
            Time.timeScale = 1;

            foreach (var disable in _disables)
                disable.OnDisable();

            _enemiesSpawner.Stop();
        }

        private void CreateGameInfrustructure()
        {
            _questsCreator = new QuestsCreator(_persistentProgress);
            IDestroyDroppableReporter destroyDroppableReporter = new DestroyDroppableReporter(_gameFactory);
            _itemsCounter = new ItemsCounter(_questsCreator, _persistentProgress);
            
            CurrencyDropper currencyDropper = new(_gameFactory, destroyDroppableReporter);
            QuestItemsDropper questItemsDropper = new(_gameFactory, destroyDroppableReporter, _questsCreator);

            _disables.Add(destroyDroppableReporter);
            _disables.Add(currencyDropper);
            _disables.Add(questItemsDropper);
        }

        private void CreateGameWorld()
        {
            _player = _gameFactory.CreatePlayer(_itemsCounter);
            _worldGenerator = _gameFactory.CreateWorldGenerator();
            _hud = _gameFactory.CreateHud(_questsCreator);
            _gameFactory.CreateVirtualCamera();
        }

        private void CreateEnemiesSpawner()
        { 
            _enemiesSpawner = new EnemiesSpawner(_coroutineRunner, _currentBiome, _staticDataService, _player.transform, _gameFactory);

            _enemiesSpawner.Start();
        }
    }
}