using Clones.Types;
using System;
using UnityEngine;

namespace Clones.EducationLogic
{
    [Serializable]
    public class WaveInfo
    {
        public EnemyType[] SpawnedEnemies;
        public int Complexity;
        public int WaveWeight;
        public Vector2 Position;
        public float MinSpawnRadius;
        public float MaxSpawnRadius;
    }
}
