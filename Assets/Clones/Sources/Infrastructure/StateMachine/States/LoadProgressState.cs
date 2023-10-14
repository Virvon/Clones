﻿using Clones.Data;
using Clones.Services;
using Clones.StaticData;
using UnityEngine;

namespace Clones.Infrastructure
{
    public class LoadProgressState : IState
    {
        private const string MainMenuScene = "MainMenu";

        private readonly IGameStateMachine _stateMachine;
        private readonly IPersistentProgressService _persistentProgressService;
        private readonly ISaveLoadService _saveLoadService;
        private readonly IMainMenuStaticDataService _mainMenuStaticDataService;

        public LoadProgressState(IGameStateMachine stateMachine, IPersistentProgressService persistentProgressService, ISaveLoadService saveLoadService, IMainMenuStaticDataService mainMenuStaticDataService)
        {
            _stateMachine = stateMachine;
            _persistentProgressService = persistentProgressService;
            _saveLoadService = saveLoadService;
            _mainMenuStaticDataService = mainMenuStaticDataService;
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
            progress.Wallet.Dna = 10000;
            progress.Wallet.Money = 10000;
            
            CreateNewCloneDatas(progress);

            return progress;
        }

        private void CreateNewCloneDatas(PlayerProgress progress)
        {
            MainMenuStaticData menuData = _mainMenuStaticDataService.GetMainMenu();

            foreach(var type in menuData.CardCloneTypes)
            {
                CardCloneStaticData cloneData = _mainMenuStaticDataService.GetCardClone(type);
                progress.CloneDatas.Add(new CloneData(cloneData.Prefab, cloneData.Type, cloneData.Helath, cloneData.Damage));
            }
        }
    }
}