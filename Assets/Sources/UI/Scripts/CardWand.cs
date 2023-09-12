using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardWand : Card
{
    [SerializeField] private float _damage;
    [SerializeField] private float _attackSpeed;

    public float Damage => _damage;
    public float AttackSpeed => _attackSpeed;
}
