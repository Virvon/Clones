using Clones.Data;
using Clones.StaticData;
using Clones.Types;

namespace Clones.UI
{
    public class ClonesCardsView : CardsView<CloneType>
    {
        public override void SelectCurrentOrDefault()
        {
            if (PersistentProgress.Progress.AvailableClones.TryGetSelectedCloneData(out CloneData cloneData) && cloneData.IsUsed == false)
                Select(GetCard(cloneData.Type));
            else
                SelectDefault();

            //ScrollRect.ScrollToCard(GetCard(PersistentProgress.Progress.AvailableClones.GetSelectedCloneData().Type));
        }

        protected override void OnBuyTried(Card card)
        {
            CloneType type = GetType(card);
            CloneStaticData cloneStaticData = MainMenuStaticDataService.GetClone(type);
            int price = cloneStaticData.BuyPrice;

            if (PersistentProgress.Progress.Wallet.TryTakeMoney(price))
            {
                PersistentProgress.Progress.AvailableClones.Clones.Add(new CloneData(type, cloneStaticData.Helath, cloneStaticData.Damage, cloneStaticData.AttackCooldown, cloneStaticData.ResourceMultiplier, cloneStaticData.UpgradePrice));
                card.Buy();
                card.GetComponent<CloneLevelView>().Init(PersistentProgress, type);
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