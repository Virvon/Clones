using UnityEngine;

namespace Clones.Infrastructure
{
    public class GameLoopState : IState
    {
        private IGameFactory _gameFactory;

        public GameLoopState(GameStateMachine stateMachine, IGameFactory gameFactory)
        {
            _gameFactory = gameFactory;
        }

        public void Enter()
        {
            GameObject player = _gameFactory.CreatePlayer();
            _gameFactory.CreateHud();
        }

        public void Exit()
        {
            
        }
    }
}