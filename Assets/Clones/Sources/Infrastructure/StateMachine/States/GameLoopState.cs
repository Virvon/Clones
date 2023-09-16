namespace Clones.Infrastructure
{
    public class GameLoopState : IState
    {
        private readonly IGameFactory _gameFactory;

        public GameLoopState(GameStateMachine stateMachine, IGameFactory gameFactory)
        {
            _gameFactory = gameFactory;
        }

        public void Enter()
        {
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