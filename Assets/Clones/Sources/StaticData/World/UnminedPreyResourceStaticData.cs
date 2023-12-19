using Clones.Types;
using UnityEngine;

namespace Clones.StaticData
{
    [CreateAssetMenu(fileName = "New UnminedResource", menuName = "Data/Create new unmined resource", order = 51)]
    public class UnminedPreyResourceStaticData : ScriptableObject
    {
        public UnminedResourceType Type;
        public GameObject Prefab;
    }
}
