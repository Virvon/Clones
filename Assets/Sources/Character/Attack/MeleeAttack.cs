using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class MeleeAttack : CharacterAttack
{
    public override float AttackSpeed => Attackble.AttackSpeed;

    protected override void Attack()
    {
        if(Target.IsAlive)
            Target.TakeDamage(Attackble.Damage);
    }
}
