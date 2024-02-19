using Clones.Data;
using Clones.Services;
using Clones.StaticData;
using Clones.Types;
using System.Collections.Generic;
using UnityEngine;

namespace Clones.Infrastructure
{
    public class LoadProgressState : IState
    {
        private const string MainMenuScene = "MainMenu";
        private const string EducationScene = "Education";

        private readonly IGameStateMachine _stateMachine;
        private readonly IPersistentProgressService _persistentProgressService;
        private readonly ISaveLoadService _saveLoadService;
        private readonly IMainMenuStaticDataService _mainMenuStaticDataService;

        private bool _isNewProgressCreated;

        public LoadProgressState(IGameStateMachine stateMachine, IPersistentProgressService persistentProgressService, ISaveLoadService saveLoadService, IMainMenuStaticDataService mainMenuStaticDataService)
        {
            _stateMachine = stateMachine;
            _persistentProgressService = persistentProgressService;
            _saveLoadService = saveLoadService;
            _mainMenuStaticDataService = mainMenuStaticDataService;

            _isNewProgressCreated = false;
        }


        public void Enter()
        {
            LoadProgressOrInitNew();

            if (_isNewProgressCreated)
                _stateMachine.Enter<LoadSceneState, string>(EducationScene, _stateMachine.Enter<EducationState>);
            else
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

            _isNewProgressCreated = true;

            return progress;
        }

        private void CreateNewAvailableClones(List<CloneData> availableClones, CloneType[] cloneTypes)
        {
            foreach(var type in cloneTypes)
            {
                CloneStaticData cloneStaticData = _mainMenuStaticDataService.GetClone(type);

                if (cloneStaticData.IsBuyed)
                    availableClones.Add(new CloneData(type, cloneStaticData.Helath, cloneStaticData.Damage, cloneStaticData.AttackCooldown, cloneStaticData.ResourceMultiplier, cloneStaticData.UpgradePrice));
            }
        }

        private void CreateNewAvailableWands(List<WandData> availableWands, WandType[] wandTypes)
        {
            foreach (var type in wandTypes)
            {
                WandStaticData wandStaticData = _mainMenuStaticDataService.GetWand(type);

                if (wandStaticData.IsBuyed)
                    availableWands.Add(new WandData(type, wandStaticData.UpgradePrice, wandStaticData.WandStats));
            }
        }
    }
}