using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    public int Health { get; private set; }
    public int Damage { get; private set; }
    public float AttackSpeed { get; private set; }

    public float MultiplierResources { get; private set; }

    public CharacterStats(int health, int damage, float attackSpeed, float multiplierResources)
    {
        Health = health;
        Damage = damage;
        AttackSpeed = attackSpeed;
        MultiplierResources = multiplierResources;
    }
}
