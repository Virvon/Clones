namespace Clones.UI
{
    public class WandUpgradeButton : UpgradeButton
    {
        public override bool CanBuy => Wallet.Money >= Price;
    }
}