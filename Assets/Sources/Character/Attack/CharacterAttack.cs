using System;
using System.Collections;
using UnityEngine;

public abstract class CharacterAttack : MonoBehaviour
{
    [SerializeField] private MonoBehaviour _attackbleBehavior;
    [SerializeField] private float _attackSpeed;

    public float AttackSpeed => _attackSpeed;

    private bool _canAttack = true;
    private Coroutine _coroutine;

    protected IAttackble Attackble { get; private set; }
    protected IDamageable Target { get; private set; }

    public event Action Attacked;
    public event Action AttackStarted;

    private void Awake() => Attackble = (IAttackble)_attackbleBehavior;

    private void OnValidate()
    {
        if(_attackbleBehavior && _attackbleBehavior is not IAttackble)
        {
            Debug.LogError(nameof(_attackbleBehavior) + " needs to implement " + nameof(IAttackble));
            _attackbleBehavior = null;
        }
    }

    public void TryAttack(IDamageable target)
    {
        if (_canAttack == false)
            return;

        Target = target;

        AttackStarted?.Invoke();

        if (_coroutine != null)
            StopCoroutine(_coroutine);

        _coroutine = StartCoroutine(CoolDownTimer(Attackble.AttackSpeed));
    }

    public void AnimationAttack()
    {
        Attack();
        Attacked?.Invoke();
    }

    protected abstract void Attack();

    private IEnumerator CoolDownTimer(float delay)
    {
        _canAttack = false;

        yield return new WaitForSeconds(delay);

        _canAttack = true;
    }
}
