using UnityEngine;

public class MeleeAttack : CharacterAttack
{
    [SerializeField] private Character _target;
    [SerializeField] private float _damage;

    public override void Attack()
    {
        _target.TakeDamage(_damage);
    }
}
