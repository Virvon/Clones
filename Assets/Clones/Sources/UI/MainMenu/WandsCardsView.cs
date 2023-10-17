using Clones.Data;
using Clones.StaticData;
using System.Collections.Generic;

public class WandsCardsView : CardsView<WandType>
{
    protected override void OnBuyTried(Card card)
    {
        WandType type = Cards.GetValueOrDefault(card);
        WandStaticData wandStaticData = MainMenuStaticDataService.GetWand(type);
        int price = wandStaticData.BuyPrice;

        if (PersistentProgress.Progress.Wallet.TryTakeMoney(price))
        {
            PersistentProgress.Progress.AvailableWands.Wands.Add(new WandData(type, wandStaticData.Damage, wandStaticData.Cooldown));
            card.Buy();
        }
    }
}