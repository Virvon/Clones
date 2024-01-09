using Clones.Data;
using Clones.Services;
using Clones.StaticData;
using UnityEngine;

namespace Clones.UI
{
    public class UpgradeButtonsView : MonoBehaviour
    {
        [SerializeField] private UpgradeButton _cloneUpgradeButton;
        [SerializeField] private UpgradeButton _wandUpgradeButton;

        private IPersistentProgressService _persistentProgress;
        private IMainMenuStaticDataService _mainMenuStaticDataService;

        private void OnDisable()
        {
            _persistentProgress.Progress.AvailableClones.SelectedCloneChanged -= OnSelectedCloneChanged;
            _persistentProgress.Progress.AvailableWands.SelectedWandChanged -= OnSelectedWandChanged;

            _cloneUpgradeButton.UpgradeTried -= UpgradeClone;
            _wandUpgradeButton.UpgradeTried -= UpgradeWand;
        }

        public void Init(IPersistentProgressService persistenProgress, IMainMenuStaticDataService mainMenuStaticDataService)
        {
            _persistentProgress = persistenProgress;
            _mainMenuStaticDataService = mainMenuStaticDataService;

            _persistentProgress.Progress.AvailableClones.SelectedCloneChanged += OnSelectedCloneChanged;
            _persistentProgress.Progress.AvailableWands.SelectedWandChanged += OnSelectedWandChanged;

            _cloneUpgradeButton.UpgradeTried += UpgradeClone;
            _wandUpgradeButton.UpgradeTried += UpgradeWand;
        }

        private void OnSelectedCloneChanged()
        {
            if (_persistentProgress.Progress.AvailableClones.TryGetSelectedCloneData(out CloneData cloneData))
                _cloneUpgradeButton.SetPrice(cloneData.UpgradePrice);
            else
                _cloneUpgradeButton.Deactive();
        }

        private void OnSelectedWandChanged() =>
            _wandUpgradeButton.SetPrice(_persistentProgress.Progress.AvailableWands.GetSelectedWandData().UpgradePrice);

        private void UpgradeClone()
        {
            if (_persistentProgress.Progress.AvailableClones.TryGetSelectedCloneData(out CloneData cloneData) && _persistentProgress.Progress.Wallet.TryTakeDna(cloneData.UpgradePrice))
            {
                CloneStaticData cloneStaticData = _mainMenuStaticDataService.GetClone(cloneData.Type);
                cloneData.Upgrade(cloneStaticData.IncreaseHealth, cloneStaticData.IncreaseDamage, cloneStaticData.IncreaseAttackCooldown, cloneStaticData.IncreaseResourceMultiplier, cloneStaticData.IncreasePrice);
                _cloneUpgradeButton.SetPrice(cloneData.UpgradePrice);
            }
        }

        private void UpgradeWand()
        {
            WandData wandData = _persistentProgress.Progress.AvailableWands.GetSelectedWandData();
            WandStaticData wandStaticData = _mainMenuStaticDataService.GetWand(wandData.Type);

            if (_persistentProgress.Progress.Wallet.TryTakeMoney(wandData.UpgradePrice))
            {
                wandData.Upgrade(wandStaticData.UpgradePriceIncrease, wandStaticData.WandStatsIncrease);
                _wandUpgradeButton.SetPrice(wandData.UpgradePrice);
            }
        }
    }
}