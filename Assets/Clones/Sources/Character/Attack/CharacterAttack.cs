using System;
using UnityEngine;

public abstract class CharacterAttack : MonoBehaviour
{
    [SerializeField] private float _cooldown;
    [SerializeField] private float _damage;

    private float _currentCooldown;
    private bool _isAttacking = false;

    protected IDamageable Target { get; private set; }
    protected float Damage => _damage;

    public event Action Attacked;
    public event Action AttackStarted;

    private void OnAttack()
    {
        Attack();
        Attacked?.Invoke();

        _isAttacking = false;
        _currentCooldown = _cooldown;
    }

    public void TryAttack(IDamageable target)
    {
        if(_currentCooldown > 0)
            _currentCooldown -= Time.deltaTime;

        if (_currentCooldown > 0 || target.IsAlive == false || _isAttacking)
            return;

        Target = target;

        _isAttacking = true;
        AttackStarted?.Invoke();
    }

    protected abstract void Attack();
}
