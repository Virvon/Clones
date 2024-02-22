using UnityEngine;

namespace Clones.EducationLogic
{
    public class SpawnFirstWaveHandler : EducationHandler
    {
        private readonly EducationEnemiesSpawner _spawner;

        public SpawnFirstWaveHandler(EducationEnemiesSpawner educationEnemiesSpawner)
        {
            _spawner = educationEnemiesSpawner;
        }

        public override void Handle()
        {
            Debug.Log("О нет, ктото приближается к нам, дай им отпор!");
            _spawner.Spawn();
            Successor.Handle();
        }
    }
}
