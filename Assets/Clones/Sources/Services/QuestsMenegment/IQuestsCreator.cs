using Clones.StaticData;
using System;
using System.Collections.Generic;

namespace Clones.Services
{
    public interface IQuestsCreator : IService
    {
        int Reward { get; }
        IReadOnlyList<Quest> Quests { get; }

        event Action Created;
        event Action<Quest> Updated;

        void Create();
        bool IsQuestItem(QuestItemType type);
        void TakeItem(QuestItemType type, int count);
    }
}