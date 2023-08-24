using UnityEngine;
using System;
using System.Collections;
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
    [SerializeField] private float _invulnerabilityTime;

    public float AttackRadius => _attackRadius;
    public float DropCollectingRadius => _dropCollectingRadius;
    public float LookRotationSpeed => _lookRotationSpeed;
    public CharacterAttack CharacterAttack => _characterAttack;
    public int Damage => 1;
    public float AttackSpeed => _movementStats.AttakcCooldown;
    public int Health => _health;
    public float MovementSpeed => _movementStats.MovementSpeed;
    //public float MovementSpeed => 10;
    public MovementStats MovementStats => _movementStats;
    public float ResourceMultiplier => 1;
    public int MaxHealth { get; private set; }
    public bool IsAlive => _isAlive;

    //private Stats _stats;
    private MovementStats _movementStats;
    private bool _isAlive;
    private bool _isInvulnerable;

    public event Action<IDamageable> Died;
    public event Action HealthChanged;

    private void Awake()
    {
        _movementStats = new MovementStats(10, _characterAttack.AttackSpeed);
        _isAlive = true;
        _isInvulnerable = false;
        MaxHealth = _health;
    }

    public void TakeDamage(float damage)
    {

        if (_isInvulnerable)
            return;

        _health -= (int)damage;

        HealthChanged?.Invoke();

        if (_health <= 0)
            Die();            
    }

    public void Reborn(int health)
    {
        StartCoroutine(InvulnerabilityTimer());

        _health = health;
        _isAlive = true;

        HealthChanged?.Invoke();
    }

    private void Die()
    {
        _isAlive = false;
        Died?.Invoke(this);
    }

    private IEnumerator InvulnerabilityTimer()
    {
        _isInvulnerable = true;

        yield return new WaitForSeconds(_invulnerabilityTime);

        _isInvulnerable = false;
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Handles.DrawWireDisc(transform.position, Vector3.up, _attackRadius);
        Handles.DrawWireDisc(transform.position, Vector3.up, _dropCollectingRadius);
    }
#endif
}
