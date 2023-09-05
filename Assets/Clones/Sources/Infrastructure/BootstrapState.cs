using System;

namespace Clones.Infrastructure
{
    public class BootstrapState : IState
    {
        private const string Scene = "Init2";

        private readonly GameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;

        public BootstrapState(GameStateMachine stateMachine, SceneLoader sceneLoader)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
        }

        public void Enter()
        {
            RegisterServices();
            _sceneLoader.Load(Scene, callback:  EnterLoadLevel);
        }

        public void Exit()
        {
            
        }

        private void RegisterServices()
        {
            Game.InputService = new MobileInputService();
        }

        private void EnterLoadLevel()
        {
            
        }
    }
}