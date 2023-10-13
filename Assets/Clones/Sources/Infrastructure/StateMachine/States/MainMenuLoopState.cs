using Clones.StaticData;
using Clones.UI;
using UnityEngine;

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
            GameObject menu = _mainMenuFactory.CreateMainMenu();

            CreateCloneCards(menu.GetComponent<MainMenu>().CardCloneTypes);
        }

        private void CreateCloneCards(CardCloneType[] types)
        {
            foreach(var type in types)
                _mainMenuFactory.CreateCardClone(type);
        }

        public void Exit()
        {
            
        }
    }
}