using System;

namespace Clones.GameLogic
{
    public interface IPlayerRevival
    {
        bool CanRivival { get; }

        bool TryRevive(Action successCallback = null, Action failureCallback = null);
    }
}