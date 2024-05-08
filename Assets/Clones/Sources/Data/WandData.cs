using Clones.Types;
using Clones.WandsBuffs;
using System;

namespace Clones.Data
{
    [Serializable]
    public class WandData
    {
        private const int StartLevel = 1;

        public WandType Type;
        public int UpgradePrice;
        public WandStats WandStats;
        public int Level;

        public event Action Upgraded;

        public WandData(WandType type, int upgradePrice, WandStats wandStats)
        {
            Type = type;
            UpgradePrice = upgradePrice;
            WandStats = wandStats;

            Level = StartLevel;
        }

        public void Upgrade(int upgradePrice, WandStatsIncrease wandStatsIncrease)
        {
            UpgradePrice += upgradePrice;
            WandStats += wandStatsIncrease;
            Level++;

            Upgraded?.Invoke();
        }
    }
}
