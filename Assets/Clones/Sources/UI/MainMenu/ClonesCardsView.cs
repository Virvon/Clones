using Clones.Data;
using Clones.StaticData;
using System.Collections.Generic;

public class ClonesCardsView : CardsView<CloneType>
{
    protected override void OnBuyTried(Card card)
    {
        CloneType type = Cards.GetValueOrDefault(card);
        CloneStaticData cloneStaticData = MainMenuStaticDataService.GetClone(type);
        int price = cloneStaticData.BuyPrice;

        if(PersistentProgress.Progress.Wallet.TryTakeMoney(price))
        {
            PersistentProgress.Progress.AvailableClones.Clones.Add(new CloneData(type, cloneStaticData.Helath, cloneStaticData.Damage));
            card.Buy();
        }
    }
}