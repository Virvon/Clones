using System.Collections.Generic;
using UnityEngine;

namespace Clones.Data
{
    [CreateAssetMenu(fileName = "New Biome", menuName = "Data/Create new biome", order = 51)]
    public class BiomeData : ScriptableObject
    {
        [SerializeField] private List<PreyResource> _preyResources;
        [SerializeField] private List<EnemyData> _enemyDatas;

        public IReadOnlyList<PreyResource> PreyResources => _preyResources;
        public IReadOnlyList<EnemyData> EnemyDatas => _enemyDatas;
    }
}
