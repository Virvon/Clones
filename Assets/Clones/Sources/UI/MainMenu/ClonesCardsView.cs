using Clones.Data;
using Clones.StaticData;
using System.Collections.Generic;
using System.Linq;

public class ClonesCardsView : CardsView<CloneType>
{
    public override void SelectCurrentOrDefault()
    {
        throw new System.NotImplementedException();
    }

    protected override void OnBuyTried(Card card)
    {
        CloneType type = GetType(card);
        CloneStaticData cloneStaticData = MainMenuStaticDataService.GetClone(type);
        int price = cloneStaticData.BuyPrice;

        if(PersistentProgress.Progress.Wallet.TryTakeMoney(price))
        {
            PersistentProgress.Progress.AvailableClones.Clones.Add(new CloneData(type, cloneStaticData.Helath, cloneStaticData.Damage));
            card.Buy();
        }
    }

    protected override void SaveCurrentCard(Card card) => 
        PersistentProgress.Progress.AvailableClones.SelectedClone = GetType(card);
}