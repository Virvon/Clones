using System;
using UnityEngine;

public interface IDamageble
{
    public event Action<IDamageble> Died;

    public abstract void TakeDamage(float damage);
}
