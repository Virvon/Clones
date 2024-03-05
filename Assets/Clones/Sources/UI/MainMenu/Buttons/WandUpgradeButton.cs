namespace Clones.UI
{
    public class WandUpgradeButton : UpgradeButton
    {
        public override bool CanUpgrade => Wallet.Money >= Price;
    }
}