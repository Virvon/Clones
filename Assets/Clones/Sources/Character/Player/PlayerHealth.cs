using UnityEngine;
using System;

public class PlayerHealth : MonoBehaviour, IDamageable, IHealthble
{
    public bool IsAlive => throw new NotImplementedException();

    public int Health => throw new NotImplementedException();
    public int MaxHealth { get; private set; }

    public event Action<IDamageable> Died;
    public event Action HealthChanged;

    public void TakeDamage(float damage)
    {
        throw new NotImplementedException();
    }
}
