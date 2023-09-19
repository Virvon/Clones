using Clones.GameLogic;
using Clones.Infrastructure;
using System;
using UnityEngine;

public class Enemy : MonoBehaviour, IDroppable, IAttackble, IHealthble
{
    public int Damage { get; private set; }
    public float AttackSpeed { get; private set; }
    public int Health => _health;
    public bool IsAlive => _isAlive;

    private bool _isAlive;
    private int _health;

    public event Action<IDamageable> Died;
    public event Action HealthChanged;

    public void Accept(IDroppableVisitor visitor) => visitor.Visit(this);

    public void Init()
    {
        _isAlive = true;
    }

    public void TakeDamage(float damage)
    {
        _health -=(int)damage;

        if (_health <= 0)
            Die();
        else
            HealthChanged?.Invoke();
    }

    private void Die()
    {
        _isAlive = false;
        Died?.Invoke(this);
        Destroy(gameObject);
    }
}
