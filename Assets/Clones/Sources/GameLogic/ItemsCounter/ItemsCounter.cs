using Clones.Services;
using System;

namespace Clones.GameLogic
{
    public class ItemsCounter : IItemsCounter
    {
        private readonly IItemVisitor _itemVisitor;

        public ItemsCounter(IQuestsCreator questsCreator, IPersistentProgressService persistenntProgress, int DNAReward, int questItemReward) => 
            _itemVisitor = new ItemVisitor(questsCreator, persistenntProgress, DNAReward, questItemReward);

        public event Action ItemTaked;

        public void TakeItem(IItem item)
        {
            item.Accept(_itemVisitor);
            ItemTaked?.Invoke();
        }

        private class ItemVisitor : IItemVisitor
        {
            private readonly IQuestsCreator _questsCreator;
            private readonly IPersistentProgressService _persistenntProgress;
            private readonly int _DNAReward;
            private readonly int _questItemReward;

            public ItemVisitor(IQuestsCreator questsCreator, IPersistentProgressService persistenntProgress, int DNAReward, int questItemReward)
            {
                _questsCreator = questsCreator;
                _persistenntProgress = persistenntProgress;
                _DNAReward = DNAReward;
                _questItemReward = questItemReward;
            }

            public void Visit(CurrencyItem currencyItem) => 
                _persistenntProgress.Progress.Wallet.CollectDna(_DNAReward);

            public void Visit(QuestItem questItem) => 
                _questsCreator.TakeItem(questItem.Type, _questItemReward);
        }
    }
}