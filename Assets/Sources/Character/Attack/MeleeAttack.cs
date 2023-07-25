using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class MeleeAttack : CharacterAttack
{
    protected override void Attack()
    {
        Target.TakeDamage(Attackble.Damage);
    }
}
