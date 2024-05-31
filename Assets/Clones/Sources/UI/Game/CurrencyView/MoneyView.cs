using Clones.Auxiliary;

namespace Clones.UI
{
    public class MoneyView : CurrencyView
    {
        protected override void UpdateCurrencyValue() =>
            CurrencyValue.text = NumberFormatter.DivideIntegerOnDigits(PersistentProgress.Progress.Wallet.Money);
    }
}