using UnityEngine;

namespace Clones.Data
{
    [CreateAssetMenu(fileName = "New Biome", menuName = "Data/Create new biome", order = 51)]
    public class BiomeData : ScriptableObject
    {
        public GameObject Prefab;
        public PreyResourceData[] PreyResourcesDatas;
        public EnemyData[] EnemyDatas;
    }
}
