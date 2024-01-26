using Clones.Infrastructure;
using Clones.Types;
using System.Collections.Generic;
using UnityEngine;

public class ShootingAttack : CharacterAttack
{
    [SerializeField] private Transform _shootingPoint;

    private BulletType _bulletType;
    private IPartsFactory _partsFactory;
    private float _damage;
    private float _cooldown;

    protected override float CoolDown => _cooldown;

    public void Init(IPartsFactory partsFactory, BulletType bulletType, int damage)
    {
        _partsFactory = partsFactory;
        _bulletType = bulletType;
        _damage = damage;
    }

    protected override void Attack()
    {
        Bullet bullet = _partsFactory.CreateBullet(_bulletType);
        bullet.Shoot(Target, gameObject, _shootingPoint, OnHitted);
    }

    private void OnHitted(List<DamageableCell> damageableCells)
    {
        foreach(var cell in damageableCells)
            cell.Damageable.TakeDamage(_damage);
    }
}