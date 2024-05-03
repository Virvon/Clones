using Clones.Data;
using Clones.StaticData;
using Clones.Types;
using System;
using System.Linq;

namespace Clones.UI
{
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
                PersistentProgress.Progress.AvailableWands.Wands.Add(new WandData(type, wandStaticData.UpgradePrice, wandStaticData.WandStats));
                SaveLoadService.SaveProgress();
                card.Buy();
            }
        }

        protected override void UpdateCurrentProgress(Card card) =>
            PersistentProgress.Progress.AvailableWands.SetSelectedWand(GetType(card));

        protected override void SelectDefault() =>
            Select(GetCard(PersistentProgress.Progress.AvailableWands.Wands.First().Type));
    }
}