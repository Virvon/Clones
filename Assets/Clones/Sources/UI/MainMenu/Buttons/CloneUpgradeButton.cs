namespace Clones.UI
{
    public class CloneUpgradeButton : UpgradeButton
    {
        public override bool CanBuy => PersistentProgress.Progress.Wallet.Dna >= Price;
    }
}