﻿using Clones.Data;
using Clones.Services;
using TMPro;
using UnityEngine;

public class StatsView : MonoBehaviour
{
    [SerializeField] private TMP_Text _health;
    [SerializeField] private TMP_Text _damage;
    [SerializeField] private TMP_Text _cooldown;
    [SerializeField] private TMP_Text _resourceMultipier;

    private IPersistentProgressService _persistentProgress;

    public void Init(IPersistentProgressService persistentProgress)
    {
        _persistentProgress = persistentProgress;

        _persistentProgress.Progress.AvailableClones.SelectedCloneChanged += UpdateStats;
        _persistentProgress.Progress.AvailableWands.SelectedWandChanged += UpdateStats;

        _persistentProgress.Progress.AvailableClones.SelectedCloneUpgraded += UpdateStats;
    }

    private void UpdateStats()
    { 
        CloneData cloneData = _persistentProgress.Progress.AvailableClones.GetSelectedCloneData();
        WandData wandData = _persistentProgress.Progress.AvailableWands.GetSelectedWandData();

        if (cloneData == null || wandData == null)
            return;

        int health = cloneData.Health;
        int damage = cloneData.Damage + wandData.Damage;
        float cooldown = wandData.Cooldown;
        //float resourceMultiplier = (_baseResourceMultiplier + _cardClone.Level * _upgradeResourceMultiplier) * (_cardClone.BaseMultiplyRecourceByRare + _cardWand.BaseMultiplyRecourceByRare);
        float resourceMultiplier = 1;

        _health.text = NumberFormatter.DivideIntegerOnDigits(health);
        _damage.text = NumberFormatter.DivideIntegerOnDigits(damage);
        _cooldown.text = NumberFormatter.DivideFloatOnDigits(cooldown);
        _resourceMultipier.text = NumberFormatter.DivideFloatOnDigits(resourceMultiplier);
    }
}