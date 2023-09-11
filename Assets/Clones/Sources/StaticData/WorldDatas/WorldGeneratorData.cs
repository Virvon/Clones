using UnityEngine;

namespace Clones.Data
{
    [CreateAssetMenu(fileName = "New WorldGenerator", menuName = "Data/Create new world generator", order = 51)]
    public class WorldGeneratorData : ScriptableObject
    {
        public WorldGenerator2 Prefab;
        public float ViewRadius;
        public float CellSize;
        public BiomeData[] BiomeDatas;
    }
}
