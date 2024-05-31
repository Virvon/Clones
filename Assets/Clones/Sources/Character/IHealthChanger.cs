using System;

public interface IHealthChanger
{
    public int Health { get; }
    public event Action HealthChanged;
}
