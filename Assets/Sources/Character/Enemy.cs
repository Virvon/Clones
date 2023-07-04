using UnityEngine;

public class Enemy : Character, ITargetable, IVisitoreble
{
    public TargetArea TargetArea { get; private set; }
    public Character Target { get; private set; }

    public Vector3 Scale => transform.localScale;

    public Stats Stats { get; private set; }

    private float _health;

    public void Accept(IVisitor visitor) => visitor.Visit(this);

    public void Init(Character target, TargetArea targetArea, Stats stats)
    {
        Target = target;
        TargetArea = targetArea;
        Stats = stats;
        _health = stats.Health;

        Debug.Log("health " + _health);
    }

    public override void TakeDamage(float damage)
    {
        _health -= damage;

        if (_health <= 0)
            Die();
    }
}
