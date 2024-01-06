using UnityEngine;

namespace Clones.StaticData
{
    [CreateAssetMenu(fileName = "New Complexity", menuName = "Data/Create new complexity", order = 51)]
    public class ComplexityStaticData : ScriptableObject
    {
        public int TargetPlayTime;
    }
}
