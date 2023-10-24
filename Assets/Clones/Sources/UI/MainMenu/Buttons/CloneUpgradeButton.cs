namespace Clones.UI
{
    public class CloneUpgradeButton : UpgradeButton
    {
        protected override bool CanUpgrade => Wallet.Dna >= Price;
    }
}