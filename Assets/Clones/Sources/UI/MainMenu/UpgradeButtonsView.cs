using Clones.Data;
using Clones.Services;
using UnityEngine;

public class UpgradeButtonsView : MonoBehaviour
{
    [SerializeField] private UpgradeButton _cloneUpgradeButton;
    [SerializeField] private UpgradeButton _wandUpgradeButton;

    private const int IncreaseHealth = 10;
    private const int IncreaseDamage = 10;
    private const int IncreaseUpgradePrice = 200;

    private IPersistentProgressService _persistentProgress;

    private void OnDisable()
    {
        _persistentProgress.Progress.AvailableClones.SelectedCloneChanged -= OnSelectedCloneChanged;
        _persistentProgress.Progress.AvailableWands.SelectedWandChanged -= OnSelectedWandChanged;

        _cloneUpgradeButton.UpgradeTried -= UpgradeClone;
        _wandUpgradeButton.UpgradeTried -= UpgradeWand;
    }

    public void Init(IPersistentProgressService persistenProgress)
    {
        _persistentProgress = persistenProgress;

        _persistentProgress.Progress.AvailableClones.SelectedCloneChanged += OnSelectedCloneChanged;
        _persistentProgress.Progress.AvailableWands.SelectedWandChanged += OnSelectedWandChanged;

        _cloneUpgradeButton.UpgradeTried += UpgradeClone;
        _wandUpgradeButton.UpgradeTried += UpgradeWand;
    }

    private void OnSelectedCloneChanged() => 
        _cloneUpgradeButton.SetPrice(_persistentProgress.Progress.AvailableClones.GetSelectedCloneData().UpgradePrice);

    private void OnSelectedWandChanged() => 
        _wandUpgradeButton.SetPrice(_persistentProgress.Progress.AvailableWands.GetSelectedWandData().UpgradePrice);

    private void UpgradeClone()
    {
        CloneData data = _persistentProgress.Progress.AvailableClones.GetSelectedCloneData();

        if (_persistentProgress.Progress.Wallet.TryTakeDna(data.UpgradePrice))
        {
            data.Upgrade(IncreaseHealth, IncreaseDamage, IncreaseUpgradePrice);
            _cloneUpgradeButton.SetPrice(data.UpgradePrice);
        }
    }

    private void UpgradeWand()
    {
        WandData data = _persistentProgress.Progress.AvailableWands.GetSelectedWandData();

        if (_persistentProgress.Progress.Wallet.TryTakeMoney(data.UpgradePrice))
        {
            data.Upgrade(IncreaseDamage, IncreaseUpgradePrice);
            _wandUpgradeButton.SetPrice(data.UpgradePrice);
        }
    }
}