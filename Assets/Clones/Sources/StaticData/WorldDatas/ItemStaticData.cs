using UnityEngine;

namespace Clones.StaticData
{
    [CreateAssetMenu(fileName = "New Item", menuName = "Data/Create new item", order = 51)]
    public class ItemStaticData : ScriptableObject
    {
        public ItemType Type;
        public GameObject Prefab;
    }
}
