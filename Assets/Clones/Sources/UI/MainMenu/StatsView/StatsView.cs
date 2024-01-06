using Clones.Data;
using Clones.Services;
using TMPro;
using UnityEngine;

namespace Clones.UI
{
    public class StatsView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _health;
        [SerializeField] private TMP_Text _damage;
        [SerializeField] private TMP_Text _attackSpeed;
        [SerializeField] private TMP_Text _resourceMultiplier;

        private IPersistentProgressService _persistentProgress;
        private IMainMenuStaticDataService _mainMenuStaticDataService;

        private void OnDisable()
        {
            _persistentProgress.Progress.AvailableClones.SelectedCloneChanged -= UpdateStats;
            _persistentProgress.Progress.AvailableWands.SelectedWandChanged -= UpdateStats;

            _persistentProgress.Progress.AvailableClones.SelectedCloneUpgraded -= UpdateStats;
            _persistentProgress.Progress.AvailableWands.SelectedWandUpgraded -= UpdateStats;
        }

        public void Init(IPersistentProgressService persistentProgress, IMainMenuStaticDataService mainMenuStaticDataService)
        {
            _persistentProgress = persistentProgress;
            _mainMenuStaticDataService = mainMenuStaticDataService;

            _persistentProgress.Progress.AvailableClones.SelectedCloneChanged += UpdateStats;
            _persistentProgress.Progress.AvailableWands.SelectedWandChanged += UpdateStats;

            _persistentProgress.Progress.AvailableClones.SelectedCloneUpgraded += UpdateStats;
            _persistentProgress.Progress.AvailableWands.SelectedWandUpgraded += UpdateStats;
        }

        private void UpdateStats()
        {
            if (_persistentProgress.Progress.AvailableClones.TryGetSelectedCloneData(out CloneData cloneData) == false)
                return;

            int health = cloneData.Health;
            int damage = cloneData.Damage;
            float attackSpeed = 1 / cloneData.AttackCooldown;
            float resourceMultiplier = cloneData.ResourceMultiplier;

            _health.text = NumberFormatter.DivideIntegerOnDigits(health);
            _damage.text = NumberFormatter.DivideIntegerOnDigits(damage);
            _attackSpeed.text = NumberFormatter.DivideFloatOnDigits(attackSpeed);
            _resourceMultiplier.text = NumberFormatter.DivideFloatOnDigits(resourceMultiplier);
        }
    }
}