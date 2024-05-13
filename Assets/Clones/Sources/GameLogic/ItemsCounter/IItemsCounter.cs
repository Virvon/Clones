using System;

namespace Clones.GameLogic
{
    public interface IItemsCounter
    {
        public event Action ItemTaked;

        void TakeItem(IItem item);
    }
}