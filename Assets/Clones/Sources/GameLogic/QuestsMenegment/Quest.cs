using Clones.StaticData;

namespace Clones.GameLogic
{
    public class Quest
    {
        public bool IsDone => TargetItemsCount <= CurrentItemsCount;
        public int CurrentItemsCount { get; private set; }
        public int TargetItemsCount { get; private set; }
        public QuestItemType Type { get; private set; }

        public Quest(QuestItemType type, int targetItemsCount)
        {
            CurrentItemsCount = 0;

            TargetItemsCount = targetItemsCount;
            Type = type;
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
