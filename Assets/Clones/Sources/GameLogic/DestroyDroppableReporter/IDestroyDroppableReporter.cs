using System;

namespace Clones.GameLogic
{
    public interface IDestroyDroppableReporter : IDisable
    {
        event Action<IDroppable> Destroyed;
    }
}