using UnityEngine;
using System;

public class PlayerHealth : MonoBehaviour, IDamageable, IHealthble
{
    [SerializeField] private int health;

    public bool IsAlive => true;

    public int Health => health;
    public int MaxHealth { get; private set; }

    public event Action<IDamageable> Died;
    public event Action HealthChanged;

    private void Start()
    {
        MaxHealth = health;
    }

    public void TakeDamage(float damage)
    {
        Debug.Log("damaged " + damage);
    }
}
