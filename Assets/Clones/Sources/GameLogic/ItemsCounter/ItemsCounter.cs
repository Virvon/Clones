using Clones.Services;
using UnityEngine;

namespace Clones.GameLogic
{
    public class ItemsCounter : IItemsCounter
    {
        private const int QuestItemCount = 1;
        private const int CurrencyItemCost = 1;

        private readonly IItemVisitor _itemVisitor;

        public ItemsCounter(IQuestsCreator questsCreator, IPersistentProgressService persistenntProgress)
        {
            _itemVisitor = new ItemVisitor(questsCreator, persistenntProgress);
        }

        public void TakeItem(IItem item) =>
            item.Accept(_itemVisitor);

        private class ItemVisitor : IItemVisitor
        {
            private readonly IQuestsCreator _questsCreator;
            private readonly IPersistentProgressService _persistenntProgress;

            public ItemVisitor(IQuestsCreator questsCreator, IPersistentProgressService persistenntProgress)
            {
                _questsCreator = questsCreator;
                _persistenntProgress = persistenntProgress;
            }

            public void Visit(CurrencyItem currencyItem)
            {
                _persistenntProgress.Progress.Wallet.CollectDna(CurrencyItemCost);
            }

            public void Visit(QuestItem questItem)
            {
                _questsCreator.TakeItem(questItem.Type, QuestItemCount);
            }
        }
    }
}