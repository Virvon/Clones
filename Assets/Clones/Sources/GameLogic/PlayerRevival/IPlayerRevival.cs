using System;

namespace Clones.GameLogic
{
    public interface IPlayerRevival
    {
        bool CanRivival { get; }

        bool TryRevive(Action callback = null);
    }
}