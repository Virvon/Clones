using Clones.GameLogic;
using Clones.Types;
using UnityEngine;

namespace Clones.StaticData
{
    [CreateAssetMenu(fileName = "New WorldGenerator", menuName = "Data/Create new world generator", order = 51)]
    public class WorldGeneratorStaticData : ScriptableObject
    {
        public WorldGenerator Prefab;
        public float ViewRadius;
        public float DestroyRadius;
        public float CellSize;
        public BiomeType[] GenerationBiomes;
    }
}
