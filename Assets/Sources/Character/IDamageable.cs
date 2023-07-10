using System;

public interface IDamageable
{
    public event Action<IDamageable> Died;

    public abstract void TakeDamage(float damage);
}
