using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Clones.Data
{
    [CreateAssetMenu(fileName = "New Spawner", menuName = "Data/Create new spawner", order = 51)]
    public class SpawnerData : ScriptableObject
    {
        [SerializeField] private float _totalWeight;
        [SerializeField] private float _cooldown;
        [SerializeField] private float _minSpawnRadius;
        [SerializeField] private float _maxSpawnRadius;

        public float TotalWeight => _totalWeight;
        public float Cooldown => _cooldown;
        public float MinSpawnRadius => _minSpawnRadius;
        public float MaxSpawnRadius => _maxSpawnRadius;
    }
}
