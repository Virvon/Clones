using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class MeleeAttack : CharacterAttack
{
    protected override void Attack()
    {
        if(Target.IsAlive)
            Target.TakeDamage(Damage);
    }
}
