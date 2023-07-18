using System;
using UnityEngine;

public abstract class Clone : MonoBehaviour, IDamageble
{
    [SerializeField] private float _health;

    public float Health => _health;

    public Vector3 Position => transform.position;

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
