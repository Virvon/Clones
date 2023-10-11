using UnityEngine;

namespace Clones.StaticData
{
    [CreateAssetMenu(fileName = "New Currency Item", menuName = "Data/Items/Create new currency item", order = 51)]
    public class CurrencyItemStaticData : ItemStaticData
    {
        public CurrencyItemType Type;
    }
}
