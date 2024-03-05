namespace Clones.UI
{
    public class CloneUpgradeButton : UpgradeButton
    {
        public override bool CanUpgrade => Wallet.Dna >= Price;
    }
}