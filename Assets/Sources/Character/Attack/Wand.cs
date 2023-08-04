using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Wand : CharacterAttack
{
    [SerializeField] private Bullet _bullet;
    [SerializeField] private float _damageMultiply;
    [SerializeField] private Transform _shootingPoint;
    [SerializeField] private float _knockbackForce;
    [SerializeField] private float _knockbackOffset;

    private event Action<List<DamageableCell>> Hitted;

    private void OnEnable() => Hitted += OnHitted;

    private void OnDisable() => Hitted -= OnHitted;

    protected override void Attack()
    {
        Bullet bullet = Instantiate(_bullet);
        bullet.Shoot(Target, (IDamageable)Attackble,_shootingPoint, Hitted);
    }

    private void OnHitted(List<DamageableCell> damageableCells)
    {
        MakeDamage(damageableCells);
        Knockback(damageableCells);
    }

    private void MakeDamage(List<DamageableCell> damageableCells)
    {
        foreach (var cell in damageableCells)
            cell.Damageable.TakeDamage(Attackble.Damage * _damageMultiply);
    }

    private void Knockback(List<DamageableCell> damageableCells)
    {
        foreach (var cell in damageableCells)
        {
            if (((MonoBehaviour)cell.Damageable).TryGetComponent(out Rigidbody rigidbody))
                rigidbody.AddForce(cell.ForceDirection * (_knockbackForce + Random.Range(-_knockbackOffset, _knockbackOffset)));
        }

    }
}
