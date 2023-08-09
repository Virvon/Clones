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

        public float TotalWeight => _totalWeight;
        public float Cooldown => _cooldown;
    }
}
