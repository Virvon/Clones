using System;

public interface IKiller
{
    public event Action<IDamageable> Killed;
}
