using UnityEngine;

namespace Clones.Data
{
    [CreateAssetMenu(fileName = "New PreyRecource", menuName = "Data/Create new prey recource", order = 51)]
    public class PreyResourceData : ScriptableObject
    {
        public PreyResource Prefab;
    }
}
