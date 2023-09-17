using Clones.GameLogic;
using System;

namespace Clones.Services
{
    public interface IDestroyDroppableReporter : IService
    {
        event Action<IDroppable> Destroyed;

        void AddDroppable(IDroppable droppable);
    }
}