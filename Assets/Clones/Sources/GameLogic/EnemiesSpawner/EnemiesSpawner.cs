using Clones.Infrastructure;
using System.Collections;

namespace Clones.GameLogic
{
    public class EnemiesSpawner : IEnemiesSpawner
    {
        private readonly ICoroutineRunner _coroutineRunner;

        public EnemiesSpawner(ICoroutineRunner coroutineRunner)
        {
            _coroutineRunner = coroutineRunner;
        }

        public void Start() =>
            _coroutineRunner.StartCoroutine(Spawner());

        public void Stop()
        {

        }

        private IEnumerator Spawner()
        {
            yield return null;
        }
    }
}