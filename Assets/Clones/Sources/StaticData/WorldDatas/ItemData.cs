using UnityEngine;

namespace Clones.StaticData
{
    [CreateAssetMenu(fileName = "New Item", menuName = "Data/Create new item", order = 51)]
    public class ItemData : ScriptableObject
    {
        [SerializeField] private Item _prefab;
        [SerializeField] private Vector3 _dropOffset;
        [SerializeField] private float _dropSpeed;

        public Item Prefab => _prefab;
        public Vector3 DropOffset => _dropOffset;
        public float DropSpeed => _dropSpeed;
    }
}
