using Clones.StaticData;
using System;
using UnityEngine;

namespace Clones.Data
{
    [Serializable]
    public class CloneData
    {
        public GameObject Prefab;
        public CardCloneType Type;
        public int Health;
        public int Damage;

        public CloneData(GameObject prefab, CardCloneType type, int health, int damage)
        {
            Prefab = prefab;
            Type = type;
            Health = health;
            Damage = damage;
        }
    }
}
