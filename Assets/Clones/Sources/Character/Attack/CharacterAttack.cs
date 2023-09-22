using System;
using UnityEngine;

public abstract class CharacterAttack : MonoBehaviour
{
    private float _currentCooldown;
    private bool _isAttacking = false;

    protected IDamageable Target { get; private set; }
    protected abstract float CoolDown { get; }

    public event Action Attacked;
    public event Action AttackStarted;

    public virtual event Action<IDamageable> Killed;

    private void OnAttack()
    {
        Attack();
        Attacked?.Invoke();

        _isAttacking = false;
        _currentCooldown = CoolDown;
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
