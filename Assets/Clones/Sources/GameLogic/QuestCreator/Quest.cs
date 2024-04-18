using Clones.StaticData;
using Clones.Types;
using UnityEngine;

namespace Clones.GameLogic
{
    public class Quest
    {
        private readonly QuestItemStaticData _staticData;

        public bool IsDone => TargetItemsCount <= CurrentItemsCount;
        public int CurrentItemsCount { get; private set; }
        public int TargetItemsCount { get; private set; }
        public QuestItemType Type => _staticData.Type;
        public Sprite Icon => _staticData.Icon;
        public string ItemName { get; private set; }

        public Quest(QuestItemStaticData staticData, int targetItemsCount, string itemName)
        {
            _staticData = staticData;
            TargetItemsCount = targetItemsCount;
            ItemName = itemName;

            CurrentItemsCount = 0;
        }

        public void TryTakeItem(QuestItemType type, int count)
        {
            if (IsDone || type != Type)
                return;

            CurrentItemsCount += count;

            if (CurrentItemsCount > TargetItemsCount)
                CurrentItemsCount = TargetItemsCount;
        }
    }
}
