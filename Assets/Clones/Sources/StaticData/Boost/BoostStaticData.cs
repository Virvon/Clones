using UnityEngine;

namespace Clones.StaticData
{
    [CreateAssetMenu(fileName = "New Boost", menuName = "Data/Create new boost", order = 51)]
    public class BoostStaticData : ScriptableObject
    {
        public GameObject Prefab;
        public BoostType Type;
    }
}
