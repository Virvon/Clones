using System;
using UnityEngine;

public abstract class Character : MonoBehaviour, IDamageble
{
    [SerializeField] private float _health;

    public float Health => _health;

    public event Action DamageTaked;
    public event Action Died;

    public void TakeDamage(float damage)
    {
        _health -= damage;

        if (_health <= 0)
        {
            Died?.Invoke();
            Die();
        }
        else
        {
            DamageTaked?.Invoke();
        }
    }

    private void Die() => Destroy(gameObject);
}
