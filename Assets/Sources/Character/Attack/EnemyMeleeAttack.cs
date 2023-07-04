using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyMeleeAttack : CharacterAttack
{
    private Enemy _enemy;

    private void Start()
    {
        _enemy = GetComponent<Enemy>();
    }

    protected override void Attack()
    {
        Target.TakeDamage(_enemy.Stats.Damage);
    }
}
