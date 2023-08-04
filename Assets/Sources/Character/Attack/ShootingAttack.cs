using System;
using System.Collections.Generic;
using UnityEngine;

public class ShootingAttack : CharacterAttack
{
    [SerializeField] private Bullet _bullet;
    [SerializeField] private Transform _shootingPoint;

    private event Action<List<IDamageable>> Hitted;

    private Vector3 _targetPosition => ((MonoBehaviour)Target).transform.position;

    private void OnEnable() => Hitted += MakeDamage;

    private void OnDisable() => Hitted -= MakeDamage;

    protected override void Attack()
    {
        Bullet bullet = Instantiate(_bullet);
        bullet.Shoot(Target, (IDamageable)Attackble,_shootingPoint, Hitted);
    }

    private void MakeDamage(List<IDamageable> damagebles)
    {
        foreach (var damageble in damagebles)
            damageble.TakeDamage(Attackble.Damage);
    }
}
