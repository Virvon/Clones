using Clones.StaticData;
using System;
using UnityEngine;

namespace Clones.Data
{
    [Serializable]
    public class WandData
    {
        public GameObject Prefab;
        public WandType Type;
        public int Damage;
        public float Cooldown;

        public WandData(GameObject prefab, WandType type, int damage, float cooldown)
        {
            Prefab = prefab;
            Type = type;
            Damage = damage;
            Cooldown = cooldown;
        }
    }
}
