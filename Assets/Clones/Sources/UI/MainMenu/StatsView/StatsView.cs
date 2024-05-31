using Clones.Auxiliary;
using Clones.Data;
using Clones.Services;
using System;
using TMPro;
using UnityEngine;

namespace Clones.UI
{
    public class StatsView : MonoBehaviour, IProgressReader
    {
        [SerializeField] private TMP_Text _health;
        [SerializeField] private TMP_Text _damage;
        [SerializeField] private TMP_Text _attackSpeed;
        [SerializeField] private TMP_Text _resourceMultiplier;

        private IPersistentProgressService _persistentProgress;

        private void OnDisable() => 
            Unsubscribe();

        public void Init(IPersistentProgressService persistentProgress)
        {
            _persistentProgress = persistentProgress;

            Subscribe();
        }

        public void UpdateProgress()
        {
            Unsubscribe();
            Subscribe();
        }

        private void UpdateStats()
        {
            WandData wandData = _persistentProgress.Progress.AvailableWands.GetSelectedWandData();

            if (_persistentProgress.Progress.AvailableClones.TryGetSelectedCloneData(out CloneData cloneData) == false || wandData == null)
                return;

            int health = cloneData.Health + (int)(cloneData.Health * wandData.WandStats.HealthIncreasePercentage / 100f);
            int damage = cloneData.Damage + (int)(cloneData.Damage * wandData.WandStats.DamageIncreasePercentage / 100f);
            float attackSpeed = 1 / (cloneData.AttackCooldown * (1 - wandData.WandStats.AttackCooldownDecreasePercentage / 100f));
            float resourceMultiplier = cloneData.ResourceMultiplier * (1 + wandData.WandStats.PreyResourcesIncreasePercentage / 100f);

            _health.text = NumberFormatter.DivideIntegerOnDigits(health);
            _damage.text = NumberFormatter.DivideIntegerOnDigits(damage);
            _attackSpeed.text = NumberFormatter.DivideFloatOnDigits(attackSpeed);
            _resourceMultiplier.text = NumberFormatter.DivideFloatOnDigits(resourceMultiplier);
        }

        private void Subscribe()
        {
            _persistentProgress.Progress.AvailableClones.SelectedCloneChanged += UpdateStats;
            _persistentProgress.Progress.AvailableWands.SelectedWandChanged += UpdateStats;

            _persistentProgress.Progress.AvailableClones.SelectedCloneUpgraded += UpdateStats;
            _persistentProgress.Progress.AvailableWands.SelectedWandUpgraded += UpdateStats;
        }

        private void Unsubscribe()
        {
            _persistentProgress.Progress.AvailableClones.SelectedCloneChanged -= UpdateStats;
            _persistentProgress.Progress.AvailableWands.SelectedWandChanged -= UpdateStats;

            _persistentProgress.Progress.AvailableClones.SelectedCloneUpgraded -= UpdateStats;
            _persistentProgress.Progress.AvailableWands.SelectedWandUpgraded -= UpdateStats;
        }
    }
}