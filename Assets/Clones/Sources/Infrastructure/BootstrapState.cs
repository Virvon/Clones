using System;

namespace Clones.Infrastructure
{
    public class BootstrapState : IState
    {
        private GameStateMachine _stateMachine;

        public BootstrapState(GameStateMachine stateMachine)
        {
            _stateMachine = stateMachine;
        }

        public void Enter()
        {
            RegisterServices();
        }

        public void Exit()
        {
            
        }

        private void RegisterServices()
        {
            Game.InputService = new MobileInputService();
        }
    }
}