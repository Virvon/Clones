using UnityEngine;

public class ShootingAttack : CharacterAttack
{
    [SerializeField] private Bullet _bullet;
    [SerializeField] private Transform _shootingPoint;

    protected override void Attack()
    {
        Bullet bullet = Instantiate(_bullet, _shootingPoint.transform.position, Quaternion.identity);
        bullet.Init(Target.transform.position - transform.position);
    }
}
