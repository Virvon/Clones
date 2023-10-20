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
        public int UpgradePrice;

        public CloneData(CloneType type, int health, int damage, int upgradePrice)
        {
            Type = type;
            Health = health;
            Damage = damage;
            UpgradePrice = upgradePrice;
        }
    }
}
