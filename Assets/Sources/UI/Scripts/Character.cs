using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clone : MonoBehaviour
{
    [SerializeField] private GameObject _prefab;
    private Stats _stats;

    public void GetStats()
    {

    }
}

public class Stats
{
    public int Health { get; private set; }
    public int Damage { get; private set; }
    public float AttackSpeed { get; private set; }

    public float MultiplierResources { get; private set; }
    public int PriceUpgrade { get; private set; }

    public Stats(int health, int damage, float attackSpeed, float multiplierResources, int priceHealthUpgrade)
    {
        Health = health;
        Damage = damage;
        AttackSpeed = attackSpeed;
        MultiplierResources = multiplierResources;
        PriceUpgrade = priceHealthUpgrade;
    }
}

public class Upgrage : MonoBehaviour
{
    [SerializeField] private DictionaryCharacters _dictionaryCharacters;

    public void UpgrageWand()
    {
        //_dictionaryCharacters.Character.Upgrade();
    }

    public void UpgrageHealth()
    {
        //_dictionaryCharacters.Character.Upgrade();
    }
}
