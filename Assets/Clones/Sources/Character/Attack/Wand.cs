using Clones.Data;
using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Wand : CharacterAttack
{
    [SerializeField] private BulletData _bulletData;
    [SerializeField] private float _damageMultiply;
    [SerializeField] private Transform _shootingPoint;
    [SerializeField] private float _knockbackForce;
    [SerializeField] private float _knockbackOffset;
    [SerializeField] private float _cooldown;
    [SerializeField] private float _damage;

    public override event Action<IDamageable> Killed;

    protected override float CoolDown => _cooldown;

    protected override void Attack()
    {
        Bullet bullet = Instantiate(_bulletData.BulletPrefab);
        bullet.Init(_bulletData);
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
            cell.Damageable.TakeDamage(_damage * _damageMultiply);

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
