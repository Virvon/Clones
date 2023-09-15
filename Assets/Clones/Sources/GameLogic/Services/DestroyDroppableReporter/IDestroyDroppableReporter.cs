using Clones.Infrastructure;
using System;

namespace Clones.GameLogic
{
    public interface IDestroyDroppableReporter : IService
    {
        event Action<IDroppable> Destroyed;
    }
}