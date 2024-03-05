using Clones.UI;
using UnityEngine;

namespace Clones.SFX
{
    public class UpgradeSound : MonoBehaviour
    {
        [SerializeField] private AudioSource _canUpgradeAudioSource;
        [SerializeField] private AudioSource _cantUpgradeAudioSource;
        [SerializeField] private UpgradeButton _upgradeButton;

        private void OnEnable() => 
            _upgradeButton.UpgradeTried += OnUpgradeTried;

        private void OnDisable() => 
            _upgradeButton.UpgradeTried -= OnUpgradeTried;

        private void OnUpgradeTried()
        {
            if (_upgradeButton.CanUpgrade)
                _canUpgradeAudioSource.Play();
            else
                _cantUpgradeAudioSource.Play();
        }
    }
}
