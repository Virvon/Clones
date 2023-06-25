using System.Collections;
using UnityEngine;

public class ShootingAttack : CharacterAttack
{
    [SerializeField] private Bullet _bullet;
    [SerializeField] private Transform _shootingPoint;
    [SerializeField] private Character _target;

    protected override void Attack()
    {
        Bullet bullet = Instantiate(_bullet, _shootingPoint.transform.position, Quaternion.identity);
        bullet.Init(_target.transform.position - transform.position);
    }
}
