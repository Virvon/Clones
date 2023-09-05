using UnityEngine;

namespace Clones.UI
{
    public class MoneyView : CurrencyView
    {
        private void OnEnable() => Wallet.MoneyCountChanged += OnCurrencyCountChanged;

        private void OnDisable() => Wallet.MoneyCountChanged -= OnCurrencyCountChanged;

        protected override void OnCurrencyCountChanged() => CurrencyValue.text = Wallet.Money.ToString();
    }
}
