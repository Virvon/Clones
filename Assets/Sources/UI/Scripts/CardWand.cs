using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardWand : Card
{
    [Space]
    [Header("Характеристики палочки")]
    [SerializeField] private float _damage;
    [SerializeField] private float _attackSpeed;

    public float Damage => _damage;
    public float AttackSpeed => _attackSpeed;

    public CardWand ReturnCard() => this;
}
