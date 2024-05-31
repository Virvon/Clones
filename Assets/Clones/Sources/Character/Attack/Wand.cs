using Clones.Infrastructure;
using Clones.Types;
using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Wand : CharacterAttack, IKiller
{
    [SerializeField] private Transform _shootingPoint;
    
    private BulletType _bulletType;
    private IPartsFactory _partsFactory;
    private float _knockbackForce;
    private float _knockbackOffset;
    private float _damage;
    private Player _player;

    protected override float CoolDown => _player.StatsProvider.GetStats().AttackCooldown;

    public event Action<IDamageable> Killed;

    public void Init(IPartsFactory partsFactory, BulletType bulletType, int damage, float knockbackForce, float knockbackOffset, Player player)
    {
        _partsFactory = partsFactory;   
        _bulletType = bulletType;
        _damage = damage;
        _knockbackForce = knockbackForce;
        _knockbackOffset = knockbackOffset;
        _player = player;
    }

    protected override void Attack()
    {
        Bullet bullet = _partsFactory.CreateBullet(_bulletType);
        bullet.Shoot(Target, gameObject, _shootingPoint, OnHitted);
    }

    private void OnHitted(List<DamageableKnockbackInfo> damageableCells)
    {
        MakeDamage(damageableCells);
        Knockback(damageableCells);
    }

    private void MakeDamage(List<DamageableKnockbackInfo> damageableCells)
    {
        foreach (var cell in damageableCells)
        {
            cell.Damageable.TakeDamage(_damage);

            if (cell.Damageable.IsAlive == false)
                Killed?.Invoke(cell.Damageable);
        }
    }

    private void Knockback(List<DamageableKnockbackInfo> damageableCells)
    {
        foreach (var cell in damageableCells)
        {
            if (((MonoBehaviour)cell.Damageable).TryGetComponent(out Knockback knockback))
                knockback.Knockbaked(cell.KnockbackDirection.normalized * (_knockbackForce + Random.Range(-_knockbackOffset, _knockbackOffset)));
        }
    }
}
