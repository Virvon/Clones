using UnityEngine;

namespace Clones.EducationLogic
{
    public class SpawnWaveHandler : EducationHandler
    {
        private readonly EducationEnemiesSpawner _spawner;

        public SpawnWaveHandler(EducationEnemiesSpawner educationEnemiesSpawner)
        {
            _spawner = educationEnemiesSpawner;
        }

        public override void Handle()
        {
            Debug.Log("О нет, ктото приближается к нам, дай им отпор!");
            _spawner.Spawn();
        }
    }
}
