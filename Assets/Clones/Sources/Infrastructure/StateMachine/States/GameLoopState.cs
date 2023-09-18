using Clones.GameLogic;
using Clones.Services;

namespace Clones.Infrastructure
{
    public class GameLoopState : IState
    {
        private readonly IGameFactory _gameFactory;
        private readonly IPersistentProgressService _persistentProgress;
        private readonly ICoroutineRunner _coroutineRunner;

        private IQuestsCreator _questsCreator;
        private IItemsCounter _itemsCounter;

        public GameLoopState(GameStateMachine stateMachine, IGameFactory gameFactory, ICoroutineRunner coroutineRunner, IPersistentProgressService persistentProgress)
        {
            _gameFactory = gameFactory;
            _coroutineRunner = coroutineRunner;
            _persistentProgress = persistentProgress;
        }

        public void Enter()
        {
            CreateGameInfrustructure();
            CreateGameWorld();           

            _questsCreator.Create();
        }

        public void Exit()
        {
            
        }

        private void CreateGameInfrustructure()
        {
            _questsCreator = new QuestsCreator(_persistentProgress);
            IDestroyDroppableReporter destroyDroppableReporter = new DestroyDroppableReporter(_gameFactory);
            _itemsCounter = new ItemsCounter(_questsCreator, _persistentProgress);
            new EnemiesSpawner(_coroutineRunner);

            new CurrencyDropper(_gameFactory, destroyDroppableReporter);
            new QuestItemsDropper(_gameFactory, destroyDroppableReporter, _questsCreator);
        }

        private void CreateGameWorld()
        {
            _gameFactory.CreatePlayer(_itemsCounter);
            _gameFactory.CreateWorldGenerator();
            _gameFactory.CreateHud(_questsCreator);
            _gameFactory.CreateVirtualCamera();
        }
    }
}