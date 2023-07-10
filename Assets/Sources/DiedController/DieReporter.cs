using System.Collections.Generic;
using UnityEngine;

public class DieReporter : MonoBehaviour
{
    [SerializeField] private CurrencyCounter _currencyCounter;

    public void TakeIDamagebles(List<IDamageable> damageables)
    {
        foreach (var damageable in damageables)
            damageable.Died += OnDied;
    }

    public void DeactivateIDamagebles(List<IDamageable> damageables)
    {
        foreach(var damageble in damageables)
            damageble.Died -= OnDied;
    }

    private void OnDied(IDamageable damageble)
    {
        if(damageble is IRewardle)
            _currencyCounter.OnKill((IRewardle )damageble);

        damageble.Died -= OnDied;
    }
}
