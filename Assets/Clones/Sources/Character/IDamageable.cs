using System;

public interface IDamageable
{
    public bool IsAlive { get; }

    public event Action<IDamageable> Died;

    public abstract void TakeDamage(float damage);
}
