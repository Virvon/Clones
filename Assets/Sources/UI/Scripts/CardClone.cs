using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardClone : MonoBehaviour
{
    [SerializeField] private GameObject _prefab;
    [SerializeField] private int _health;
    [SerializeField] private int _damage;
    [SerializeField] private float _attackSpeed;
    [SerializeField] private float _multiplierResources;
    [SerializeField] private int _priceUpgrade;
    [SerializeField] private int _secondsRecovery;

    private Stats _stats;

    private const int StartLevel = 1;

    private void Awake()
    {
        if (_stats == null)
            _stats = new Stats(StartLevel, _health, _damage, _attackSpeed, _multiplierResources, _priceUpgrade, _secondsRecovery);
    }

    public Stats GetStats() => _stats;
}

public class Stats
{
    public int Level { get; private set; }
    public int Health { get; private set; }
    public int Damage { get; private set; }
    public float AttackSpeed { get; private set; }

    public float MultiplierResources { get; private set; }
    public int PriceUpgrade { get; private set; }
    public int SecondsRecovery { get; private set; }
    public float RemainingSecondsRecovery { get; private set; }

    public Stats(int level, int health, int damage, float attackSpeed, float multiplierResources, int priceHealthUpgrade, int secondsRecovery)
    {
        Level = level;
        Health = health;
        Damage = damage;
        AttackSpeed = attackSpeed;
        MultiplierResources = multiplierResources;
        PriceUpgrade = priceHealthUpgrade;
        SecondsRecovery = secondsRecovery;
        RemainingSecondsRecovery = 0f;
    }
}
