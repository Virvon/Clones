using Clones.StaticData;
using System;
using UnityEngine;

namespace Clones.Data
{
    [Serializable]
    public class WandData
    {
        public int Damage;
        public float Cooldown;

        public WandData(int damage, float cooldown)
        {
            Damage = damage;
            Cooldown = cooldown;
        }
    }
}
