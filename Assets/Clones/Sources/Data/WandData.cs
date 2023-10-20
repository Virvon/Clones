using Clones.StaticData;
using System;

namespace Clones.Data
{
    [Serializable]
    public class WandData
    {
        public WandType Type;
        public int Damage;
        public float Cooldown;
        public int UpgradePrice;

        public WandData(WandType type, int damage, float cooldown, int upgradePrice)
        {
            Type = type;
            Damage = damage;
            Cooldown = cooldown;
            UpgradePrice = upgradePrice;
        }
    }
}
