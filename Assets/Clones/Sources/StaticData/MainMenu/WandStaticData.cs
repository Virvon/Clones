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
        public int Damage;
        public float Cooldown;
        public int UpgradePrice;
        public int BuyPrice;
        public bool IsBuyed;
    }
}
