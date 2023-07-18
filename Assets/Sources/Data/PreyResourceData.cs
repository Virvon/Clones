using System.Collections.Generic;
using UnityEngine;

namespace Clones.Data
{
    [CreateAssetMenu(fileName = "New PreyRecource", menuName = "Data/Create new prey recource", order = 51)]
    public class PreyResourceData : ItemData
    {
        [SerializeField] private PreyResource _preyResourcePrefab;
        [SerializeField] private List<ItemData> _itemDatas;

        public PreyResource PreyResourcePrefab => _preyResourcePrefab;
        public IReadOnlyList<ItemData> ItemDatas => _itemDatas;
    }
}
