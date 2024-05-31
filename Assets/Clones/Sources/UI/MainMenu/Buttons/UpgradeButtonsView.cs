using Clones.Data;
using Clones.Services;
using Clones.StaticData;
using UnityEngine;

namespace Clones.UI
{
    public class UpgradeButtonsView : MonoBehaviour, IProgressReader
    {
        [SerializeField] private UpgradeButton _cloneUpgradeButton;
        [SerializeField] private UpgradeButton _wandUpgradeButton;

        private IPersistentProgressService _persistentProgress;
        private IMainMenuStaticDataService _mainMenuStaticDataService;
        private ISaveLoadService _saveLoadService;

        private void OnDisable()
        {
            UnsubscribeFromProgress();

            _cloneUpgradeButton.BuyTried -= UpgradeClone;
            _wandUpgradeButton.BuyTried -= UpgradeWand;
        }

        public void Init(IPersistentProgressService persistenProgress, IMainMenuStaticDataService mainMenuStaticDataService, ISaveLoadService saveLoadService)
        {
            _persistentProgress = persistenProgress;
            _mainMenuStaticDataService = mainMenuStaticDataService;
            _saveLoadService = saveLoadService;

            SubscribeToProgress();

            _cloneUpgradeButton.BuyTried += UpgradeClone;
            _wandUpgradeButton.BuyTried += UpgradeWand;
        }

        public void UpdateProgress()
        {
            UnsubscribeFromProgress();
            SubscribeToProgress();
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
                float decreaseAttackCooldow = cloneData.AttackCooldown - cloneStaticData.IncreaseAttackCooldown >= cloneStaticData.MinAttackCooldow ? cloneStaticData.IncreaseAttackCooldown : cloneData.AttackCooldown - cloneStaticData.MinAttackCooldow;

                cloneData.Upgrade(cloneStaticData.IncreaseHealth, cloneStaticData.IncreaseDamage, decreaseAttackCooldow, cloneStaticData.IncreaseResourceMultiplier, cloneStaticData.IncreasePrice);
                _saveLoadService.SaveProgress();
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
                _saveLoadService.SaveProgress();
                _wandUpgradeButton.SetPrice(wandData.UpgradePrice);
            }
        }

        private void SubscribeToProgress()
        {
            _persistentProgress.Progress.AvailableClones.SelectedCloneChanged += OnSelectedCloneChanged;
            _persistentProgress.Progress.AvailableWands.SelectedWandChanged += OnSelectedWandChanged;
        }

        private void UnsubscribeFromProgress()
        {
            _persistentProgress.Progress.AvailableClones.SelectedCloneChanged -= OnSelectedCloneChanged;
            _persistentProgress.Progress.AvailableWands.SelectedWandChanged -= OnSelectedWandChanged;
        }
    }
}