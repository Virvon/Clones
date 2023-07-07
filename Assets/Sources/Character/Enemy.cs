using System;
using UnityEngine;

public class Enemy : MonoBehaviour, IRewardle , IDamageble, IAttackble
{
    public PlayerArea TargetArea { get; private set; }
    public Player Target { get; private set; }
    public Vector3 Scale => transform.localScale;
    public Vector3 Position => transform.position;
    public Stats Stats { get; private set; }
    public int Damage => Stats.Damage;
    public float AttackSpeed => Stats.AttackSpeed;


    private float _health;

    public event Action<IDamageble> Died;

    public void Accept(IVisitor visitor) => visitor.Visit(this);

    public void Init(Player target, PlayerArea targetArea, Stats stats)
    {
        Target = target;
        TargetArea = targetArea;
        Stats = stats;
        _health = stats.Health;
    }

    public void TakeDamage(float damage)
    {
        _health -= damage;

        if (_health <= 0)
            Die();
    }

    private void Die()
    {
        Died?.Invoke(this);
        Destroy(gameObject);
    }
}
