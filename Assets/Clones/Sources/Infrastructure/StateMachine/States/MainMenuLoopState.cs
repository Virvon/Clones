using Clones.UI;
using System;

namespace Clones.Infrastructure
{
    public class MainMenuLoopState : IState
    {
        private readonly GameStateMachine _stateMachine;
        private readonly IMainMenuFactory _mainMenuFactory;

        public MainMenuLoopState(GameStateMachine stateMachine, IMainMenuFactory mainMenuFactory)
        {
            _stateMachine = stateMachine;
            _mainMenuFactory = mainMenuFactory;
        }

        public void Enter()
        {
            _mainMenuFactory.CreateMainMenu();
        }

        public void Exit()
        {
            
        }
    }
}