using UnityEngine;

namespace Clones.Data
{
    [CreateAssetMenu(fileName = "New PreyRecource", menuName = "Data/Create new prey recource", order = 51)]
    public class PreyResourceData : ScriptableObject
    {
        [SerializeField] private PreyResource _prefab;
        [SerializeField] private ItemData _itemData;

        public PreyResource Prefab => _prefab;
        public ItemData ItemData => _itemData;
    }
}
