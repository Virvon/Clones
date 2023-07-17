using System;

public interface IHealthble
{
    public int Health { get; }

    public event Action DamageTaked;
}
