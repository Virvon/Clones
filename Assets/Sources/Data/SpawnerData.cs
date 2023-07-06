using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Clones.Data
{
    [CreateAssetMenu(fileName = "New Spawner", menuName = "Data/Create new spawner", order = 51)]
    public class SpawnerData : ScriptableObject
    {
        [SerializeField] private float _baseTotalWeight;

        public float BaseTotalWeight => _baseTotalWeight;
    }
}
