using Clones.Types;
using Clones.WandsBuffs;
using System;
using UnityEngine;

namespace Clones.Data
{
    [Serializable]
    public class WandData
    {
        public WandType Type;
        public int UpgradePrice;
        public WandStats WandStats;

        public event Action Upgraded;

        public WandData(WandType type, int upgradePrice, WandStats wandStats)
        {
            Type = type;
            UpgradePrice = upgradePrice;
            WandStats = wandStats;
        }

        public void Upgrade(int upgradePrice, WandStatsIncrease wandStatsIncrease)
        {
            UpgradePrice += upgradePrice;
            WandStats += wandStatsIncrease;
            Debug.Log(WandStats.AttackCooldownDecreasePercentage);

            Upgraded?.Invoke();
        }
    }
}
