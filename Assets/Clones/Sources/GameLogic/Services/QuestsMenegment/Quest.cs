using Clones.StaticData;

namespace Clones.GameLogic
{
    public class Quest
    {
        public bool IsDone => _targetItemsCount > _currentItemsCount;
        public ItemType Type { get; private set; }

        private readonly int _targetItemsCount;

        private int _currentItemsCount;

        public Quest(ItemType type, int targetItemsCount)
        {
            _currentItemsCount = 0;

            _targetItemsCount = targetItemsCount;
            Type = type;
        }

        public void TryTakeItem(ItemType type, int count)
        {
            if (IsDone || type != Type)
                return;

            _currentItemsCount += count;
        }
    }
}
