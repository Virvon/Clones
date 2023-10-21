using Clones.Data;
using Clones.Services;
using Clones.StaticData;
using System.Collections.Generic;
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

            CreateNewAvailableClones(progress.AvailableClones.Clones, menuData.CloneTypes);
            CreateNewAvailableWands(progress.AvailableWands.Wands, menuData.WandTypes);

            return progress;
        }

        private void CreateNewAvailableClones(List<CloneData> availableClones, CloneType[] cloneTypes)
        {
            foreach(var type in cloneTypes)
            {
                CloneStaticData cloneStaticData = _mainMenuStaticDataService.GetClone(type);

                if (cloneStaticData.IsBuyed)
                    availableClones.Add(Test(cloneStaticData, type));
            }
        }

        private void CreateNewAvailableWands(List<WandData> availableWands, WandType[] wandTypes)
        {
            foreach (var type in wandTypes)
            {
                WandStaticData wamdStaticData = _mainMenuStaticDataService.GetWand(type);

                if (wamdStaticData.IsBuyed)
                    availableWands.Add(new WandData(type, wamdStaticData.Damage, wamdStaticData.UpgradePrice));
            }
        }

        private CloneData Test(CloneStaticData cloneStaticData, CloneType type)
        {
            CloneData data = new CloneData(type, cloneStaticData.Helath, cloneStaticData.Damage, cloneStaticData.UpgradePrice);
            data.Use(cloneStaticData.DisuseTime);

            return data;
        }
    }
}