using Clones.Types;
using Clones.UI;
using Clones.WandsBuffs;
using System;
using UnityEngine;

namespace Clones.StaticData
{
    [CreateAssetMenu(fileName = "New Wand", menuName = "Data/Create new wand", order = 51)]
    public class WandStaticData : ScriptableObject
    {
        public GameObject Prefab;
        public WandCard Card;
        public WandType Type;
        public BulletType Bullet;
        public int UpgradePrice;
        public int UpgradePriceIncrease;
        public float KnockbackForse;
        public float KnockbackOffset;
        public int BuyPrice;
        public bool IsBuyed;
        public WandStats WandStats;
        public WandStatsIncrease WandStatsIncrease;
    }
}
