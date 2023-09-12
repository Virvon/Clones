using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardClone : Card
{
    [SerializeField] private float _helath;
    [SerializeField] private float _damage;

    public float Helath => _helath;
    public float Damage => _damage;
}
