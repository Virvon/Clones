namespace Clones.UI
{
    public class CloneUpgradeButton : UpgradeButton
    {
        public override bool CanBuy => Wallet.Dna >= Price;
    }
}