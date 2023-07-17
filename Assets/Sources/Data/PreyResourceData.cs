using UnityEngine;

namespace Clones.Data
{
    [CreateAssetMenu(fileName = "New PreyRecource", menuName = "Data/Create new prey recource", order = 51)]
    public class PreyResourceData : ItemData
    {
        [SerializeField] private PreyResourceType _type;

        public PreyResourceType Type => _type;
    }
}
