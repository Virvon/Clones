using UnityEngine;

public class MeleeAttack : CharacterAttack
{
    [SerializeField] private float _damage;

    protected override void Attack()
    {
        Target.TakeDamage(_damage);
    }
}
