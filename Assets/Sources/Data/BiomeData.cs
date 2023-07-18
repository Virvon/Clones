using System.Collections.Generic;
using UnityEngine;

namespace Clones.Data
{
    [CreateAssetMenu(fileName = "New Biome", menuName = "Data/Create new biome", order = 51)]
    public class BiomeData
    {
        [SerializeField] private GameObject _tilePrefab;
        [SerializeField] private List<PreyResource> _preyResources;
        [SerializeField] private List<EnemyData> _enemyDatas;

        public GameObject TilePrefab => _tilePrefab;
        public List<PreyResource> PreyResources => _preyResources;
        public List<EnemyData> EnemyDatas => _enemyDatas;
    }
}
