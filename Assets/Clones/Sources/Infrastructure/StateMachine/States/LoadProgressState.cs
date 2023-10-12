using Clones.Data;
using Clones.Services;
using UnityEngine;

namespace Clones.Infrastructure
{
    public class LoadProgressState : IState
    {
        private const string MainMenuScene = "MainMenu";

        private readonly IGameStateMachine _stateMachine;
        private readonly IPersistentProgressService _persistentProgressService;
        private readonly ISaveLoadService _saveLoadService;

        public LoadProgressState(IGameStateMachine stateMachine, IPersistentProgressService persistentProgressService, ISaveLoadService saveLoadService)
        {
            _stateMachine = stateMachine;
            _persistentProgressService = persistentProgressService;
            _saveLoadService = saveLoadService;
        }


        public void Enter()
        {
            LoadProgressOrInitNew();

            _stateMachine.Enter<LoadSceneState, string>(MainMenuScene, _stateMachine.Enter<MainMenuLoopState>);
        }

        public void Exit()
        {
            
        }

        private void LoadProgressOrInitNew() => 
            _persistentProgressService.Progress = _saveLoadService.LoadProgress() ?? CreateNewProgress();

        private PlayerProgress CreateNewProgress()
        {
            Debug.Log("created new progress");
            PlayerProgress progress = new();

            return progress;
        }
    }
}