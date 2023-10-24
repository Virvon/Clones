namespace Clones.UI
{
    public class DnaView : CurrencyView
    {
        protected override void UpdateCurrencyValue() =>
            CurrencyValue.text = Wallet.Dna.ToString();
    }
}