using Clones.Items;
using System;

namespace Clones.GameLogic
{
    public interface IItemsCounter : IScoreable
    {
        public event Action ItemTaked;

        void TakeItem(IItem item);
    }
}