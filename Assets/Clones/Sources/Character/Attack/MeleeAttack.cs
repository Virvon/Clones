using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class MeleeAttack : CharacterAttack
{
    private float _cooldown;
    private float _damage;

    protected override float CoolDown => _cooldown;

    public void Init(float damage, float cooldown)
    {
        _damage = damage;
        _cooldown = cooldown;
    }

    protected override void Attack()
    {
        if(Target.IsAlive)
            Target.TakeDamage(_damage);
    }
}
