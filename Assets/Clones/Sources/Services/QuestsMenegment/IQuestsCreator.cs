using Clones.Infrastructure;
using Clones.StaticData;
using System;
using System.Collections.Generic;

namespace Clones.Services
{
    public interface IQuestsCreator : IService
    {
        IReadOnlyList<Quest> Quests { get; }

        event Action Created;
        event Action<Quest> Updated;

        bool IsQuestItem(ItemType type);
    }
}