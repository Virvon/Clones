using System;

namespace Clones.Character
{
    public interface IDamageable
    {
        public event Action<IDamageable> Died;
        public bool IsAlive { get; }

        public abstract void TakeDamage(float damage);
    }
}