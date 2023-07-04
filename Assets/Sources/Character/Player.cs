using UnityEngine;
using System;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class Player : Character
{
    [SerializeField] private float _attackRadius;
    [SerializeField] private float _lookRotationSpeed;
    [SerializeField] private CharacterAttack _characterAttack;
    [SerializeField] private float _health;

    public float AttackRadius => _attackRadius;
    public float LookRotationSpeed => _lookRotationSpeed;
    public CharacterAttack CharacterAttack => _characterAttack;

    public override void TakeDamage(float damage)
    {
        _health -= damage;

        if (_health <= 0)
            Die();
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Handles.DrawWireDisc(transform.position, Vector3.up, _attackRadius);
    }
#endif
}
