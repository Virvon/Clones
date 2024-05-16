namespace Clones.UI
{
    public class DnaView : CurrencyView
    {
        protected override void UpdateCurrencyValue() =>
            CurrencyValue.text = NumberFormatter.DivideIntegerOnDigits(PersistentProgress.Progress.Wallet.Dna);
    }
}