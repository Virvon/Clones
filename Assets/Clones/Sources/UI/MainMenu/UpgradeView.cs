using Clones.Services;
using UnityEngine;

public class UpgradeView : MonoBehaviour
{
    [SerializeField] private UpgradeButton _cloneUpgradeButton;
    [SerializeField] private UpgradeButton _wandUpgradeButton;

    private IPersistentProgressService _persistentProgress;

    public void Init(IPersistentProgressService persistenProgress)
    {
        _persistentProgress = persistenProgress;

        _persistentProgress.Progress.AvailableClones.SelectedCloneChanged += OnSelectedCloneChanged;
        _persistentProgress.Progress.AvailableWands.SelectedWandChanged += OnSelectedWandChanged;
    }

    private void OnSelectedCloneChanged() => 
        _cloneUpgradeButton.SetPrice(_persistentProgress.Progress.AvailableClones.GetSelectedCloneData().UpgradePrice);

    private void OnSelectedWandChanged() => 
        _wandUpgradeButton.SetPrice(_persistentProgress.Progress.AvailableWands.GetSelectedWandData().UpgradePrice);

    private void UpgradeClone()
    {
        Debug.Log("clone upgraded");
    }
}