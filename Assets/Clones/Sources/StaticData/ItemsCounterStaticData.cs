using UnityEngine;

namespace Clones.StaticData
{
    [CreateAssetMenu(fileName = "New CurrencyCounter", menuName = "Data/Create new currency counter", order = 51)]
    public class ItemsCounterStaticData : ScriptableObject
    {
        public int DNAReward;
        public int CollectingItemReward;
    }
}
