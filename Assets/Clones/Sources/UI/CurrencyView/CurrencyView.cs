using TMPro;
using UnityEngine;

namespace Clones.UI
{
    public abstract class CurrencyView : MonoBehaviour
    {
        [SerializeField] private Wallet _wallet;
        [SerializeField] private TMP_Text _currencyValue;

        protected Wallet Wallet => _wallet; 
        protected TMP_Text CurrencyValue => _currencyValue;

        protected abstract void OnCurrencyCountChanged();
    }
}
