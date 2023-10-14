using Clones.Data;
using Clones.Services;
using Clones.StaticData;
using System.Collections.Generic;
using UnityEngine;

namespace Clones.Infrastructure
{
    public class MainMenuLoopState : IState
    {
        private readonly GameStateMachine _stateMachine;
        private readonly IMainMenuFactory _mainMenuFactory;
        private readonly IMainMenuStaticDataService _mainMenuStaticDataService;

        public MainMenuLoopState(GameStateMachine stateMachine, IMainMenuFactory mainMenuFactory, IMainMenuStaticDataService mainMenuStaticDataService)
        {
            _stateMachine = stateMachine;
            _mainMenuFactory = mainMenuFactory;
            _mainMenuStaticDataService = mainMenuStaticDataService;
        }

        public void Enter()
        {
            _mainMenuFactory.CreateMainMenu();

            CreateCloneCards(_mainMenuStaticDataService.GetMainMenu().CardCloneTypes);
        } 

        public void Exit()
        {
            
        }

        private void CreateCloneCards(CardCloneType[] types)
        {
            foreach (var type in types)
                _mainMenuFactory.CreateCardClone(type);
        }
    }
}