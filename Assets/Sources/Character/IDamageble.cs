using System;
using UnityEngine;

public interface IDamageble
{
    public Vector3 Position { get; }

    public event Action<IDamageble> Died;

    public abstract void TakeDamage(float damage);
}
