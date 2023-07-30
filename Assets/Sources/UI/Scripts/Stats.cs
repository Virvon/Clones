using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats : MonoBehaviour
{
    [SerializeField] private int _level;
    [SerializeField] private int _health;
    [SerializeField] private int _damage;
    [SerializeField] private float _attackSpeed;
    [SerializeField] private float _multiplierResources;
    [SerializeField] private int _secondsRecovery;
    
    private float _remainingSecondsRecovery;

    public int Level => _level;
    public int Health => _health;
    public int Damage => _damage;
    public float AttackSpeed => _attackSpeed;
    public float MultiplierResources => _multiplierResources;
    public float SecondsRecovery => _secondsRecovery;
    public float RemainingSecondsRecovery => _remainingSecondsRecovery;


    public Stats(int level, int health, int damage, float attackSpeed, float multiplierResources, int secondsRecovery)
    {
        _level = level;
        _health = health;
        _damage = damage;
        _attackSpeed = attackSpeed;
        _multiplierResources = multiplierResources;
        _secondsRecovery = secondsRecovery;
        _remainingSecondsRecovery = 0f;
    }

    public void Upgrade(int health, int damage, float attackSpeed, float multiplierResources)
    {
        _level++;
        _health += health;
        _damage += damage;
        _attackSpeed += attackSpeed;
        _multiplierResources += multiplierResources;
    }
}
