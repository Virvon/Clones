using Clones.Types;
using System;
using System.Collections.Generic;

namespace Clones.GameLogic
{
    public interface IQuestsCreator : IScoreable
    {
        event Action Created;
        event Action<Quest> Updated;
        event Action Completed;

        int Reward { get; }
        float Complexity { get; }
        IReadOnlyList<Quest> Quests { get; }

        void Create();
        bool IsQuestItem(QuestItemType type);
        void TakeItem(QuestItemType type, int count);
    }
}