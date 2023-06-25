using System;
using UnityEngine;

public class Character : MonoBehaviour 
{
    [SerializeField] private float _health;

    public event Action Died;

    public void Attack(float damage)
    {
        _health -= damage;

        if (_health <= 0)
            Died?.Invoke();
    }
}