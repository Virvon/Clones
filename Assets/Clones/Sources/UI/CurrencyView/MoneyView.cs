namespace Clones.UI
{
    public class MoneyView : CurrencyView
    {
        protected override void UpdateCurrencyValue() => 
            CurrencyValue.text = Wallet.Money.ToString();
    }
}
