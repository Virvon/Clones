using Clones.Types;
using System;
using System.Collections.Generic;

namespace Clones.GameLogic
{
    public interface IQuestsCreator
    {
        int Reward { get; }
        IReadOnlyList<Quest> Quests { get; }

        event Action Created;
        event Action<Quest> Updated;
        event Action Completed;

        void Create();
        bool IsQuestItem(QuestItemType type);
        void TakeItem(QuestItemType type, int count);
    }
}