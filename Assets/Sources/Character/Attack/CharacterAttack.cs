using System.Collections;
using UnityEngine;

public abstract class CharacterAttack : MonoBehaviour
{
    [SerializeField] private float _coolDown;

    private bool _canAttack = true;
    private Coroutine _coroutine;

    protected IDamageble Target { get; private set; }

    public void TryAttack(IDamageble target)
    {
        if (_canAttack == false)
            return;

        Target = target;

        Attack();

        if (_coroutine != null)
            StopCoroutine(_coroutine);

        _coroutine = StartCoroutine(CoolDownTimer(_coolDown));
    }

    protected abstract void Attack();

    private IEnumerator CoolDownTimer(float delay)
    {
        _canAttack = false;

        yield return new WaitForSeconds(delay);

        _canAttack = true;
    }
}
