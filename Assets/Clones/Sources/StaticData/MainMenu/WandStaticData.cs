using Clones.Types;
using Clones.UI;
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
        public int Damage;
        public float Cooldown;
        public int UpgradePrice;
        public float KnockbackForse;
        public float KnockbackOffset;
        public int BuyPrice;
        public bool IsBuyed;
    }
}
