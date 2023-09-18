using System;

namespace Clones.GameLogic
{
    public interface IDestroyDroppableReporter
    {
        event Action<IDroppable> Destroyed;

        void AddDroppable(IDroppable droppable);
    }
}