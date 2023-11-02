using Clones.Types;
using UnityEngine;

namespace Clones.StaticData
{
    [CreateAssetMenu(fileName = "New Quest Item", menuName = "Data/Items/Create new quest item", order = 51)]
    public class QuestItemStaticData : ItemStaticData
    {
        public QuestItemType Type;
    }
}
