using Clones.GameLogic;
using System;
using UnityEngine;

public class EnemyHealth : MonoBehaviour, IDroppable, IHealthble
{
    public int Health { get; private set; }
    public bool IsAlive { get; private set; }

    public event Action HealthChanged;
    public event Action<IDamageable> Died;

    public void Accept(IDroppableVisitor visitor) => 
        visitor.Visit(GetComponent<Enemy>());

    public void Init(int health)
    {
        Health = health;

        IsAlive = true;
    }

    public void TakeDamage(float damage)
    {
        Health -= (int)damage;

        HealthChanged?.Invoke();

        if (Health <= 0)
        {
            IsAlive = false;
            Health = 0;
            Died?.Invoke(this);
            Destroy(gameObject);
        }
    }

    public void Disappear()
    {
        IsAlive = false;
        Destroy(gameObject);
    }
}