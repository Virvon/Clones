using System;

namespace Clones.Character.Attack
{
    public interface IKiller
    {
        public event Action<IDamageable> Killed;
    }
}