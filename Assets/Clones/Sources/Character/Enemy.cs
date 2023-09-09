using System;
using UnityEngine;

public class Enemy : MonoBehaviour, IDropble , IDamageable, IAttackble, IHealthble
{
    public GameObject Target { get; private set; }
    public Vector3 Scale => transform.localScale;
    public Vector3 Position => transform.position;
    public Stats Stats { get; private set; }
    public int Damage => Stats.Damage;
    public float AttackSpeed => Stats.AttackSpeed;
    public int Health => _health;
    public float KnockbackForce => 800;
    public bool IsAlive => _isAlive;

    private bool _isAlive;
    private int _health;

    public event Action<IDamageable> Died;
    public event Action HealthChanged;

    public void Accept(IDropVisitor visitor) => visitor.Visit(this);

    public void Init(GameObject target, Stats stats)
    {
        Target = target;
        Stats = stats;
        _health = stats.Health;
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
