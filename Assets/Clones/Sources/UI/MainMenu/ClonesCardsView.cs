using Clones.Data;
using Clones.StaticData;
using System.Linq;

public class ClonesCardsView : CardsView<CloneType>
{
    public override void SelectCurrentOrDefault()
    {
        CloneType type = PersistentProgress.Progress.AvailableClones.SelectedClone;

        if (type != CloneType.Undefined)
        {
            CloneData selectedCloneData = PersistentProgress.Progress.AvailableClones.GetSelectedCloneData();

            if(selectedCloneData.IsUsed == false)
            {
                Select(GetCard(type));
                return;
            }
        }

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

    protected override void SelectDefault()
    {
        CloneType availableType = CloneType.Undefined;

        foreach(var type in Types.Values)
        {
            if (PersistentProgress.Progress.AvailableClones.Clones.Any(data => data.Type == type && data.IsUsed == false))
            {
                availableType = type;

                break;
            }
        }

        if (availableType != CloneType.Undefined)
            Select(GetCard(availableType));
        else
            PersistentProgress.Progress.AvailableClones.SetSelectedClone(availableType);
    }
}