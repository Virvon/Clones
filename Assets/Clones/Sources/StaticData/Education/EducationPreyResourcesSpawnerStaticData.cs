using Clones.EducationLogic;
using UnityEngine;

namespace Clones.StaticData
{
    [CreateAssetMenu(fileName = "New Education Prey Resources Spawner", menuName = "Data/Create new education prey resources spawner", order = 51)]
    public class EducationPreyResourcesSpawnerStaticData : ScriptableObject
    {
        public EducationPreyResourcesSpawner Prefab;
        public PreyResourceCell[] PreyResourceCells;
    }
}