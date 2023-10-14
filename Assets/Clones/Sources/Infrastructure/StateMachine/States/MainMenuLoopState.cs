using Clones.Data;
using Clones.Services;
using Clones.StaticData;
using Clones.UI;
using System.Collections.Generic;
using UnityEngine;

namespace Clones.Infrastructure
{
    public class MainMenuLoopState : IState
    {
        private readonly GameStateMachine _stateMachine;
        private readonly IMainMenuFactory _mainMenuFactory;
        private readonly IPersistentProgressService _persistentProgress;

        public MainMenuLoopState(GameStateMachine stateMachine, IMainMenuFactory mainMenuFactory, IPersistentProgressService persistentProgress)
        {
            _stateMachine = stateMachine;
            _mainMenuFactory = mainMenuFactory;
            _persistentProgress = persistentProgress;
        }

        public void Enter()
        {
            GameObject menu = _mainMenuFactory.CreateMainMenu();

            CreateCloneCards(_persistentProgress.Progress.CloneDatas);
        } 

        public void Exit()
        {
            
        }

        private void CreateCloneCards(List<CloneData> cloneDatas)
        {
            foreach (var data in cloneDatas)
                _mainMenuFactory.CreateCardClone(data.Type);
        }
    }
}