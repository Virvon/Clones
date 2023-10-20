public class WandUpgradeButton : UpgradeButton
{
    protected override bool CanUpgrade => Wallet.Money >= Price;
}