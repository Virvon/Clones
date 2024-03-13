using Clones.UI;
using UnityEngine;

namespace Clones.SFX
{
    public class BuySound : MonoBehaviour
    {
        [SerializeField] private AudioSource _canUpgradeAudioSource;
        [SerializeField] private AudioSource _cantUpgradeAudioSource;
        [SerializeField] private MonoBehaviour _buyableBehaviour;

        private IBuyable _buyable;

        private void OnValidate()
        {
            if(_buyableBehaviour && _buyableBehaviour is not IBuyable)
            {
                Debug.LogError(nameof(_buyableBehaviour) + " needs to implement " + nameof(IBuyable));
                _buyableBehaviour = null;
            }
        }

        private void Awake() => 
            _buyable = (IBuyable)_buyableBehaviour;

        private void OnEnable() =>
            _buyable.BuyTried += OnBuyTried;

        private void OnDisable() =>
            _buyable.BuyTried -= OnBuyTried;

        private void OnBuyTried()
        {
            if (_buyable.CanBuy)
                _canUpgradeAudioSource.Play();
            else
                _cantUpgradeAudioSource.Play();
        }
    }
}
