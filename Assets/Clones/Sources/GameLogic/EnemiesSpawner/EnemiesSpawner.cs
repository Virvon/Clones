using Clones.Infrastructure;
using System;
using System.Collections;
using UnityEngine;

namespace Clones.GameLogic
{
    public class EnemiesSpawner : IEnemiesSpawner
    {
        public const float StartDelay = 3;
        public const float SpawnCooldown = 3;

        private readonly ICoroutineRunner _coroutineRunner;

        private bool _isFinish;

        public EnemiesSpawner(ICoroutineRunner coroutineRunner)
        {
            _coroutineRunner = coroutineRunner;
        }


        public void Start()
        {
            _isFinish = false;

            _coroutineRunner.StartCoroutine(Spawner());
        }

        public void Stop()
        {
            _isFinish = true;
        }

        private void CreateWave()
        {

        }

        private IEnumerator Spawner()
        {
            yield return new WaitForSeconds(StartDelay);

            while(_isFinish == false)
            {
                CreateWave();

                yield return new WaitForSeconds(SpawnCooldown);
            }
        }
    }
}