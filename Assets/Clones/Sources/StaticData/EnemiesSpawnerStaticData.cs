using UnityEngine;

namespace Clones.StaticData
{
    [CreateAssetMenu(fileName = "New Spawner", menuName = "Data/Create new spawner", order = 51)]
    public class EnemiesSpawnerStaticData : ScriptableObject
    {
        public GameObject Prefab;
        public float StartDelay;
        public float SpawnCooldown;
        public float WaveWeight;
        public float MinRadius;
        public float MaxRadius;
        public float SpeedIncreasePerWave;
        public int MaxWavesWithSpeedIncrease;
    }
}
