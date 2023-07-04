using System;
using UnityEngine;

public class Enemy : MonoBehaviour, ITargetable, IVisitoreble, IDamageble
{
    public TargetArea TargetArea { get; private set; }
    public Player Target { get; private set; }
    public Vector3 Scale => transform.localScale;
    public Vector3 Position => throw new NotImplementedException();
    public Stats Stats { get; private set; }

    private float _health;

    public event Action<IDamageble> Died;

    public void Accept(IVisitor visitor) => visitor.Visit(this);

    public void Init(Player target, TargetArea targetArea, Stats stats)
    {
        Target = target;
        TargetArea = targetArea;
        Stats = stats;
        _health = stats.Health;

        Debug.Log("health " + _health);
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
