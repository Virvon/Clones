using UnityEngine;

namespace Clones.StaticData
{
    [CreateAssetMenu(fileName = "New PreyRecource", menuName = "Data/Create new prey recource", order = 51)]
    public class PreyResourceStaticData : ScriptableObject
    {
        public PreyResourceType Type;
        public PreyResource Prefab;
    }
}
