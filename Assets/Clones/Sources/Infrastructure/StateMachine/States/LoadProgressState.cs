using Clones.Data;
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

            MainMenuStaticData menuData = _mainMenuStaticDataService.GetMainMenu();
            CreateNewCloneDatas(progress, menuData);
            CreateNewWandDatas(progress, menuData);

            return progress;
        }

        private void CreateNewCloneDatas(PlayerProgress progress, MainMenuStaticData menuData)
        {
            foreach(var type in menuData.CardCloneTypes)
            {
                CardCloneStaticData cloneData = _mainMenuStaticDataService.GetCardClone(type);
                progress.CloneDatas.Add(new CloneData(cloneData.Prefab, cloneData.Type, cloneData.Helath, cloneData.Damage));
            }
        }

        private void CreateNewWandDatas(PlayerProgress progress, MainMenuStaticData menuData)
        {
            foreach(var type in menuData.WandTypes)
            {
                WandStaticData wandData = _mainMenuStaticDataService.GetWand(type);
                progress.WandDatas.Add(new WandData(wandData.Prefab, wandData.Type, wandData.Damage, wandData.Cooldown));
            }
        }
    }
}