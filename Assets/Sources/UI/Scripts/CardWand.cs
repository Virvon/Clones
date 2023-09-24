using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardWand : Card
{
    [Space]
    [Header("Характеристики палочки")]
    [SerializeField] private int _damage;
    [SerializeField] private float _attackSpeed;

    public int Damage => _damage;
    public float AttackSpeed => _attackSpeed;

    public CardWand ReturnCard() => this;
}
