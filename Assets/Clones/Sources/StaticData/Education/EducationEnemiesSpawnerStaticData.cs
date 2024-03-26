using Clones.EducationLogic;
using UnityEngine;

namespace Clones.StaticData
{
    [CreateAssetMenu(fileName = "New Education Enemies Spawner", menuName = "Data/Create new education enemies spawner", order = 51)]
    public class EducationEnemiesSpawnerStaticData : ScriptableObject
    {
        public EducationEnemiesSpawner Prefab;       
        public WaveInfo[] WaveInfos;
    }
}