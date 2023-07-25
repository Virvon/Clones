using System;
using UnityEngine;

public class Enemy : MonoBehaviour, IDropble , IDamageable, IAttackble, IHealthble
{
    public PlayerArea TargetArea { get; private set; }
    public Player Target { get; private set; }
    public Vector3 Scale => transform.localScale;
    public Vector3 Position => transform.position;
    public Stats Stats { get; private set; }
    public int Damage => Stats.Damage;
    public float AttackSpeed => Stats.AttackSpeed;
    public int Health => _health;
    public float KnockbackForce => 800;

    private int _health;

    public event Action<IDamageable> Died;
    public event Action DamageTaked;

    public void Accept(IDropVisitor visitor) => visitor.Visit(this);

    public void Init(Player target, PlayerArea targetArea, Stats stats)
    {
        Target = target;
        TargetArea = targetArea;
        Stats = stats;
        _health = stats.Health;
    }

    public void TakeDamage(float damage)
    {
        _health -=(int)damage;

        if (_health <= 0)
            Die();
        else
            DamageTaked?.Invoke();
    }

    private void Die()
    {
        Died?.Invoke(this);
        Destroy(gameObject);
    }
}
