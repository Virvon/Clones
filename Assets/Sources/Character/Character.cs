using System;
using System.Collections;
using UnityEngine;

public abstract class Character : MonoBehaviour, IDamageble
{
    public Vector3 Position => transform.position;

    public event Action DamageTaked;
    public event Action<IDamageble> Died;

    public virtual void TakeDamage(float damage)
    {
        throw new NotImplementedException();
    }

    protected void Die()
    {
        Died?.Invoke(this);
        Destroy(gameObject);
    }
}
