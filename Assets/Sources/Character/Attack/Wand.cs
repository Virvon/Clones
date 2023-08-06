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

    private event Action<List<DamageableCell>> Hitted;

    private Vector3 x;
    private Vector3 y;

    private void OnEnable() => Hitted += OnHitted;

    private void OnDisable() => Hitted -= OnHitted;

    protected override void Attack()
    {
        Bullet bullet = Instantiate(_bulletData.BulletPrefab);
        bullet.Init(_bulletData);
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
        x = ((MonoBehaviour)damageableCells[0].Damageable).transform.position;
        y = damageableCells[0].KnockbackDirection;

        if (name == "magic wand")
            Debug.Log(damageableCells[0].KnockbackDirection);

        foreach (var cell in damageableCells)
        {
            if (((MonoBehaviour)cell.Damageable).TryGetComponent(out Rigidbody rigidbody))
                rigidbody.AddForce(cell.KnockbackDirection.normalized * (_knockbackForce + Random.Range(-_knockbackOffset, _knockbackOffset)));
        }
    }

    private void OnDrawGizmos()
    {
        if (name != "magic wand")
            return;

        Gizmos.color = Color.red;
        Gizmos.DrawLine(x, y + x);
    }
}
