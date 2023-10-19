using Clones.StaticData;
using System;

namespace Clones.Data
{
    [Serializable]
    public class CloneData
    {
        public CloneType Type;
        public int Health;
        public int Damage;

        public CloneData(CloneType type, int health, int damage)
        {
            Type = type;
            Health = health;
            Damage = damage;
        }
    }
}
