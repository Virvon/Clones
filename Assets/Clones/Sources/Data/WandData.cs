using Clones.Types;
using System;

namespace Clones.Data
{
    [Serializable]
    public class WandData
    {
        public WandType Type;
        public int UpgradePrice;

        public event Action Upgraded;

        public WandData(WandType type, int upgradePrice)
        {
            Type = type;
            UpgradePrice = upgradePrice;
        }

        public void Upgrade(int upgradePrice)
        {
            UpgradePrice += upgradePrice;

            Upgraded?.Invoke();
        }
    }
}
