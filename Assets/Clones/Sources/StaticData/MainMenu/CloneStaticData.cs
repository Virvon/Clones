﻿using Clones.Types;
using Clones.UI;
using UnityEngine;

namespace Clones.StaticData
{
    [CreateAssetMenu(fileName = "New CardClone", menuName = "Data/Create new card clone", order = 51)]
    public class CloneStaticData : ScriptableObject
    {
        public CloneType Type;
        public GameObject Prefab;
        public GameObject Model;
        public CloneCard Card;
        public int Helath;
        public int Damage;
        public float AttackCooldown;
        public float ResourceMultiplier;
        public int UpgradePrice;
        public int IncreaseHealth;
        public int IncreaseDamage;
        public float IncreaseAttackCooldown;
        public float IncreaseResourceMultiplier;
        public int IncreasePrice;
        public float MinAttackCooldow;
        public int DisuseTime;
        public float MovementSpeed;
        public float RotationSpeed;
        public float MiningRadius;
        public float AttackRadius;
        public float DropCollectingRadius;
        public int BuyPrice;
        public bool IsBuyed;
        public GameObject RebornEffect;
        public Vector3 RebornEffectOffset;
    }
}