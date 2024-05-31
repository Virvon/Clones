using Clones.GameLogic;
using System;

namespace Clones.Character.Attack
{
    public interface IKiller : IScoreable
    {
        event Action<IDamageable> Killed;
    }
}