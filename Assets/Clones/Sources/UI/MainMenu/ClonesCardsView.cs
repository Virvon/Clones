using Clones.Data;
using Clones.StaticData;

namespace Clones.UI
{
    public class ClonesCardsView : CardsView<CloneType>
    {
        public override void SelectCurrentOrDefault()
        {
            CloneData selectedCloneData = PersistentProgress.Progress.AvailableClones.GetSelectedCloneData();

            if (selectedCloneData != null && selectedCloneData.IsUsed == false)
                Select(GetCard(selectedCloneData.Type));
            else
                SelectDefault();
        }

        protected override void OnBuyTried(Card card)
        {
            CloneType type = GetType(card);
            CloneStaticData cloneStaticData = MainMenuStaticDataService.GetClone(type);
            int price = cloneStaticData.BuyPrice;

            if (PersistentProgress.Progress.Wallet.TryTakeMoney(price))
            {
                PersistentProgress.Progress.AvailableClones.Clones.Add(new CloneData(type, cloneStaticData.Helath, cloneStaticData.Damage, cloneStaticData.UpgradePrice));
                card.Buy();
            }
        }

        protected override void SaveCurrentCard(Card card) =>
            PersistentProgress.Progress.AvailableClones.SetSelectedClone(GetType(card));

        protected override void SelectDefault()
        {
            if (PersistentProgress.Progress.AvailableClones.TryGetFirstDisuse(out CloneType type))
                Select(GetCard(type));
            else
                PersistentProgress.Progress.AvailableClones.SetSelectedClone(CloneType.Undefined);
        }
    }
}