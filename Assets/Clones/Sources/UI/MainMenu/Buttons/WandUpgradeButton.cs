namespace Clones.UI
{
    public class WandUpgradeButton : UpgradeButton
    {
        public override bool CanBuy => PersistentProgress.Progress.Wallet.Money >= Price;
    }
}