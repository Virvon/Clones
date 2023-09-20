using Clones.GameLogic;
using Clones.Services;
using Clones.StaticData;
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
        private GameObject _player;
        private WorldGenerator _worldGenerator;

        public GameLoopState(GameStateMachine stateMachine, IGameFactory gameFactory, ICoroutineRunner coroutineRunner, IPersistentProgressService persistentProgress, IStaticDataService staticDataService)
        {
            _gameFactory = gameFactory;
            _coroutineRunner = coroutineRunner;
            _persistentProgress = persistentProgress;
            _staticDataService = staticDataService;
        }

        public void Enter()
        {
            CreateGameInfrustructure();
            CreateGameWorld();           

            _questsCreator.Create();
            _currentBiome = new CurrentBiome(_worldGenerator);

            CreateEnemiesSpawner();
        }

        public void Exit()
        {
            
        }

        private void CreateGameInfrustructure()
        {
            _questsCreator = new QuestsCreator(_persistentProgress);
            IDestroyDroppableReporter destroyDroppableReporter = new DestroyDroppableReporter(_gameFactory);
            _itemsCounter = new ItemsCounter(_questsCreator, _persistentProgress);
            
            new CurrencyDropper(_gameFactory, destroyDroppableReporter);
            new QuestItemsDropper(_gameFactory, destroyDroppableReporter, _questsCreator);
        }

        private void CreateGameWorld()
        {
            _player = _gameFactory.CreatePlayer(_itemsCounter);
            _worldGenerator = _gameFactory.CreateWorldGenerator();
            _gameFactory.CreateHud(_questsCreator);
            _gameFactory.CreateVirtualCamera();
        }

        private void CreateEnemiesSpawner()
        { 
            IEnemiesSpawner spawner = new EnemiesSpawner(_coroutineRunner, _currentBiome, _staticDataService, _player.transform, _gameFactory);

            spawner.Start();
        }
    }
}