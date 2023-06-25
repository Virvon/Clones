using System;
using UnityEngine;

public class Character : MonoBehaviour, IDamageble
{
    [SerializeField] private float _health;

    public float Health => _health;

    public event Action Died;
    public event Action DamageTaked;

    public void TakeDamage(float damage)
    {
        _health -= damage;

        if (_health <= 0)
            Died?.Invoke();
        else
            DamageTaked?.Invoke();

        Debug.Log(_health);
    }
}