using Clones.Data;
using Clones.StaticData;

public class ClonesCardsView : CardsView<CloneType>
{
    public override void SelectCurrentOrDefault()
    {
        CloneType type = PersistentProgress.Progress.AvailableClones.SelectedClone;

        if (type != CloneType.Undefined)
            Select(GetCard(type));
        else
            SelectDefault();
    }

    protected override void OnBuyTried(Card card)
    {
        CloneType type = GetType(card);
        CloneStaticData cloneStaticData = MainMenuStaticDataService.GetClone(type);
        int price = cloneStaticData.BuyPrice;

        if(PersistentProgress.Progress.Wallet.TryTakeMoney(price))
        {
            PersistentProgress.Progress.AvailableClones.Clones.Add(new CloneData(type, cloneStaticData.Helath, cloneStaticData.Damage, cloneStaticData.UpgradePrice));
            card.Buy();
        }
    }

    protected override void SaveCurrentCard(Card card) => 
        PersistentProgress.Progress.AvailableClones.SetSelectedClone(GetType(card));
}