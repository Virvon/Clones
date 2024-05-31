﻿using Clones.Items;
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
        public event Action Scored;

        public void TakeItem(IItem item)
        {
            item.Accept(_itemVisitor);
            ItemTaked?.Invoke();
            Scored?.Invoke();
        }

        private class ItemVisitor : IItemVisitor
        {
            private readonly IQuestsCreator _questsCreator;
            private readonly IPersistentProgressService _persistenntProgress;
            private readonly int _dnaReward;
            private readonly int _questItemReward;

            public ItemVisitor(IQuestsCreator questsCreator, IPersistentProgressService persistenntProgress, int DNAReward, int questItemReward)
            {
                _questsCreator = questsCreator;
                _persistenntProgress = persistenntProgress;
                _dnaReward = DNAReward;
                _questItemReward = questItemReward;
            }

            public void Visit(CurrencyItem currencyItem) => 
                _persistenntProgress.Progress.Wallet.CollectDna(_dnaReward);

            public void Visit(QuestItem questItem) => 
                _questsCreator.TakeItem(questItem.Type, _questItemReward);
        }
    }
}