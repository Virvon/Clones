using UnityEngine;
using System;

public class PlayerHealth : MonoBehaviour, IDamageable, IHealthble
{
    [SerializeField] private int _health;

    public bool IsAlive => true;

    public int Health => _health;
    public int MaxHealth { get; private set; }

    public event Action<IDamageable> Died;
    public event Action HealthChanged;

    private void Start()
    {
        MaxHealth = _health;
    }

    public void TakeDamage(float damage)
    {
        _health -= (int)damage;

        if(_health < 0)
            _health = 0;

        HealthChanged?.Invoke();

        if (_health == 0)
        {
            Died?.Invoke(this);
            gameObject.SetActive(false);
        }
    }

    public void Reborn(int health)
    {
        gameObject.SetActive(true);
        _health = health;
        HealthChanged?.Invoke();
    }
}
