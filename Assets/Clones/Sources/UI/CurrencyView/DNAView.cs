namespace Clones.UI
{
    public class DNAView : CurrencyView
    {
        private void OnEnable() => Wallet.DNACountChanged += OnCurrencyCountChanged;

        private void OnDisable() => Wallet.DNACountChanged -= OnCurrencyCountChanged;

        protected override void OnCurrencyCountChanged() => CurrencyValue.text = Wallet.DNA.ToString();
    }
}
