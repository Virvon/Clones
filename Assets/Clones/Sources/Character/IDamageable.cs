using System;

namespace Clones.Character
{
    public interface IDamageable
    {
        public bool IsAlive { get; }
        public event Action<IDamageable> Died;

        public abstract void TakeDamage(float damage);
    }
}