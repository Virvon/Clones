using UnityEngine;

namespace Clones.Data
{
    [CreateAssetMenu(fileName = "New Resource Drop", menuName = "Data/Create new resource drop", order = 51)]
    public class ResourceDropData : ScriptableObject
    {
        [SerializeField] private float _radius;
        [SerializeField] private int _maxItemsCount;

        public float Radius => _radius;
        public int MaxItemsCount => _maxItemsCount;
    }
}
