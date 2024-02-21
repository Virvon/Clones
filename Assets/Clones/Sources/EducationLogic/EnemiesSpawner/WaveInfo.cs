using Clones.Types;
using System;

namespace Clones.EducationLogic
{
    [Serializable]
    public class WaveInfo
    {
        public EnemyType[] SpawnedEnemies;
        public int Complexity;
        public int WaveWeight;
    }
}
