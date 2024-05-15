using System; 
using UnityEngine;

public abstract class CharacterAttack : MonoBehaviour
{
    private float _currentCooldown;

    protected IDamageable Target { get; private set; }
    protected abstract float CoolDown { get; }

    public event Action Attacked;
    public event Action AttackStarted;

    private void Update()
    {
        if (_currentCooldown > 0)
            _currentCooldown -= Time.deltaTime;
    }

    private void OnAttack()
    {
        Attack();
        Attacked?.Invoke();
    }

    public void TryAttack(IDamageable target)
    {
        if (_currentCooldown > 0 || target.IsAlive == false)
            return;

        _currentCooldown = CoolDown;
        Target = target;

        AttackStarted?.Invoke();
    }

    protected abstract void Attack();
}
