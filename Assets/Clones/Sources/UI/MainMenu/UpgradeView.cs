using Clones.Services;
using System;
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

    private void OnSelectedCloneChanged()
    {
        
    }

    private void OnSelectedWandChanged()
    {

    }

    private void UpgradeClone()
    {
        Debug.Log("clone upgraded");
    }
}