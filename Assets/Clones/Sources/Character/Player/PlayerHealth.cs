using UnityEngine;
using System;

public class PlayerHealth : MonoBehaviour, IDamageable, IHealthble
{
    [SerializeField] private GameObject _rebornEffect;
    [SerializeField] private Vector3 _effectOffset;

    public bool IsAlive => true;

    public int Health { get; private set; }
    public int MaxHealth { get; private set; }

    public event Action<IDamageable> Died;
    public event Action HealthChanged;
    public event Action DamageTaked;

    public void Init(int health)
    {
        Health = health;
        MaxHealth = Health;
    }

    public void TakeDamage(float damage)
    {
        Health -= (int)damage;

        if(Health < 0)
            Health = 0;

        HealthChanged?.Invoke();
        DamageTaked?.Invoke();

        if (Health == 0)
        {
            Died?.Invoke(this);
            gameObject.SetActive(false);
        }
    }

    public void Reborn(int health)
    {
        Instantiate(_rebornEffect, transform.position + _effectOffset, Quaternion.identity);
        gameObject.SetActive(true);
        Health = health;
        HealthChanged?.Invoke();
    }
}
