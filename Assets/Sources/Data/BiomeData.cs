using System.Collections.Generic;
using UnityEngine;

namespace Clones.Data
{
    [CreateAssetMenu(fileName = "New Biome", menuName = "Data/Create new biome", order = 51)]
    public class BiomeData : ScriptableObject
    {
        [SerializeField] private List<PreyResourceData> _preyResourceDatas;
        [SerializeField] private List<EnemyData> _enemyDatas;

        public IReadOnlyList<PreyResourceData> PreyResourcesDatas => _preyResourceDatas;
        public IReadOnlyList<EnemyData> EnemyDatas => _enemyDatas;
    }
}
