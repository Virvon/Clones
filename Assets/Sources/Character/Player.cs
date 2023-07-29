using UnityEngine;
using System;
#if UNITY_EDITOR
using UnityEditor;
#endif


public class Player : MonoBehaviour, IDamageable, IAttackble, IHealthble
{
    [SerializeField] private float _attackRadius;
    [SerializeField] private float _dropCollectingRadius;
    [SerializeField] private float _lookRotationSpeed;
    [SerializeField] private CharacterAttack _characterAttack;
    [SerializeField] private int _health;

    public float AttackRadius => _attackRadius;
    public float DropCollectingRadius => _dropCollectingRadius;
    public float LookRotationSpeed => _lookRotationSpeed;
    public CharacterAttack CharacterAttack => _characterAttack;
    public int Damage => 0;
    public float AttackSpeed => _movementStats.AttakcSpeed;
    public int Health => _health;
    public float MovementSpeed => _movementStats.MovementSpeed;
    public MovementStats MovementStats => _movementStats;
    public float KnockbackForce => 800;

    //private Stats _stats;
    private MovementStats _movementStats = new MovementStats(10, 0.78f);

    public event Action<IDamageable> Died;
    public event Action DamageTaked;

    public void TakeDamage(float damage)
    {
        _health -= (int)damage;

        if (_health <= 0)
            Die();
        else
            DamageTaked?.Invoke();
    }

    private void Die()
    {
        Died?.Invoke(this);
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Handles.DrawWireDisc(transform.position, Vector3.up, _attackRadius);
        Handles.DrawWireDisc(transform.position, Vector3.up, _dropCollectingRadius);
    }
#endif
}
