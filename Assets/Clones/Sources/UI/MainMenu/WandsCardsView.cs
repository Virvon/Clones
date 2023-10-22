using Clones.Data;
using Clones.StaticData;
using System.Linq;

public class WandsCardsView : CardsView<WandType>
{
    public override void SelectCurrentOrDefault()
    {
        WandType type = PersistentProgress.Progress.AvailableWands.SelectedWand;

        if (type != WandType.Undefined)
            Select(GetCard(type));
        else
            SelectDefault();
    }

    protected override void OnBuyTried(Card card)
    {
        WandType type = GetType(card);
        WandStaticData wandStaticData = MainMenuStaticDataService.GetWand(type);
        int price = wandStaticData.BuyPrice;

        if (PersistentProgress.Progress.Wallet.TryTakeMoney(price))
        {
            PersistentProgress.Progress.AvailableWands.Wands.Add(new WandData(type, wandStaticData.Damage, wandStaticData.UpgradePrice));
            card.Buy();
        }
    }

    protected override void SaveCurrentCard(Card card) => 
        PersistentProgress.Progress.AvailableWands.SetSelectedWand(GetType(card));

    protected override void SelectDefault()
    {
        foreach(var type in Types.Values)
        {
            if(PersistentProgress.Progress.AvailableWands.Wands.Any(data => data.Type == type))
            {
                Select(GetCard(type));

                break;
            }
        }
    }
}