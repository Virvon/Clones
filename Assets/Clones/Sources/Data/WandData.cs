using Clones.Types;
using System;

namespace Clones.Data
{
    [Serializable]
    public class WandData
    {
        public WandType Type;
        public int Damage;
        public int UpgradePrice;

        public event Action Upgraded;

        public WandData(WandType type, int damage, int upgradePrice)
        {
            Type = type;
            Damage = damage;
            UpgradePrice = upgradePrice;
        }

        public void Upgrade(int damage, int upgradePrice)
        {
            Damage += damage;
            UpgradePrice += upgradePrice;

            Upgraded?.Invoke();
        }
    }
}
