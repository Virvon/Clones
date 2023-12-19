using Clones.Infrastructure;
using Clones.Types;
using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Wand : CharacterAttack
{
    [SerializeField] private Transform _shootingPoint;
    
    private BulletType _bulletType;
    private IPartsFactory _partsFactory;
    private float _knockbackForce;
    private float _knockbackOffset;
    private float _damage;
    private IStatsProvider _statsProvider;

    public override event Action<IDamageable> Killed;

    protected override float CoolDown => _statsProvider.GetStats().AttackCooldown;

    public void Init(IPartsFactory partsFactory, BulletType bulletType, int damage, float knockbackForce, float knockbackOffset, IStatsProvider statsProvider)
    {
        _partsFactory = partsFactory;   
        _bulletType = bulletType;
        _damage = damage;
        _knockbackForce = knockbackForce;
        _knockbackOffset = knockbackOffset;
        _statsProvider = statsProvider;
    }

    protected override void Attack()
    {
        Bullet bullet = _partsFactory.CreateBullet(_bulletType);
        bullet.Shoot(Target, gameObject, _shootingPoint, OnHitted);
    }

    private void OnHitted(List<DamageableCell> damageableCells)
    {
        MakeDamage(damageableCells);
        Knockback(damageableCells);
    }

    private void MakeDamage(List<DamageableCell> damageableCells)
    {
        foreach (var cell in damageableCells)
        {
            cell.Damageable.TakeDamage(_damage);

            if (cell.Damageable.IsAlive == false)
                Killed?.Invoke(cell.Damageable);
        }
    }

    private void Knockback(List<DamageableCell> damageableCells)
    {
        foreach (var cell in damageableCells)
        {
            if (((MonoBehaviour)cell.Damageable).TryGetComponent(out Knockback knockback))
                knockback.Knockbaked(cell.KnockbackDirection.normalized * (_knockbackForce + Random.Range(-_knockbackOffset, _knockbackOffset)));
        }
    }
}
