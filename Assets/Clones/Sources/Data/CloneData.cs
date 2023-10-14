using Clones.StaticData;
using System;
using UnityEngine;

namespace Clones.Data
{
    [Serializable]
    public class CloneData
    {
        public int Health;
        public int Damage;

        public CloneData(int health, int damage)
        {
            Health = health;
            Damage = damage;
        }
    }
}
