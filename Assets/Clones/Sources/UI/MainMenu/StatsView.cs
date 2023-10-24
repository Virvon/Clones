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
        [SerializeField] private TMP_Text _cooldown;
        [SerializeField] private TMP_Text _resourceMultipier;

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
            WandData wandData = _persistentProgress.Progress.AvailableWands.GetSelectedWandData();

            if (_persistentProgress.Progress.AvailableClones.TryGetSelectedCloneData(out CloneData cloneData) == false || wandData == null)
                return;

            int health = cloneData.Health;
            int damage = cloneData.Damage + wandData.Damage;
            float cooldown = GetCooldown();
            //float resourceMultiplier = (_baseResourceMultiplier + _cardClone.Level * _upgradeResourceMultiplier) * (_cardClone.BaseMultiplyRecourceByRare + _cardWand.BaseMultiplyRecourceByRare);
            float resourceMultiplier = 1;

            _health.text = NumberFormatter.DivideIntegerOnDigits(health);
            _damage.text = NumberFormatter.DivideIntegerOnDigits(damage);
            _cooldown.text = NumberFormatter.DivideFloatOnDigits(cooldown);
            _resourceMultipier.text = NumberFormatter.DivideFloatOnDigits(resourceMultiplier);
        }

        private float GetCooldown() =>
            _mainMenuStaticDataService.GetWand(_persistentProgress.Progress.AvailableWands.SelectedWand).Cooldown;
    }
}