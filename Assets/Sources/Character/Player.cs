using UnityEngine;
using System;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class Player : MonoBehaviour, IDamageble, IAttackble
{
    [SerializeField] private float _attackRadius;
    [SerializeField] private float _lookRotationSpeed;
    [SerializeField] private CharacterAttack _characterAttack;
    [SerializeField] private float _health;

    public float AttackRadius => _attackRadius;
    public float LookRotationSpeed => _lookRotationSpeed;
    public CharacterAttack CharacterAttack => _characterAttack;
    public Vector3 Position => transform.position;
    public int Damage => 2;

    //private Stats _stats;

    public event Action<IDamageble> Died;

    public void TakeDamage(float damage)
    {
        _health -= damage;

        if (_health <= 0)
            Die();
    }

    private void Die()
    {
        Died?.Invoke(this);
        Destroy(gameObject);
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Handles.DrawWireDisc(transform.position, Vector3.up, _attackRadius);
    }
#endif
}
