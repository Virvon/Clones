using Clones.GameLogic;
using Clones.Services;

namespace Clones.Infrastructure
{
    public class GameLoopState : IState
    {
        private readonly IGameFactory _gameFactory;
        private readonly IDestroyDroppableReporter _destroyDroppableReporter;
        private readonly IQuestsCreator _questsCreator;

        public GameLoopState(GameStateMachine stateMachine, IGameFactory gameFactory, IDestroyDroppableReporter destroyDroppableReporter, IQuestsCreator questsCreator)
        {
            _gameFactory = gameFactory;
            _destroyDroppableReporter = destroyDroppableReporter;
            _questsCreator = questsCreator;
        }

        public void Enter()
        {
            new CurrencyDropper(_gameFactory, _destroyDroppableReporter);
            new QuestItemsDropper(_gameFactory, _destroyDroppableReporter, _questsCreator);

            _gameFactory.CreatePlayer();
            _gameFactory.CreateWorldGenerator();
            _gameFactory.CreateHud();
            _gameFactory.CreateVirtualCamera();
        }

        public void Exit()
        {
            
        }
    }
}