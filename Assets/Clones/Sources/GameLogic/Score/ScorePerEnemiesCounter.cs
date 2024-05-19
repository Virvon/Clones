using System;

namespace Clones.GameLogic
{
    public class ScorePerEnemiesCounter : IScoreCounter
    {
        private readonly IKiller _killer;
        private readonly EnemiesSpawner _enemiesSpawner;
        private readonly Complexity _complexity;

        private readonly int _scorePerKill;

        public int Score { get; private set; }

        public event Action ScoreUpdated;

        public ScorePerEnemiesCounter(IKiller killer, EnemiesSpawner enemiesSpawner, Complexity complexity, int scorePerKill)
        {
            _killer = killer;
            _enemiesSpawner = enemiesSpawner;
            _complexity = complexity;
            _scorePerKill = scorePerKill;

            _killer.Killed += OnKilled;
        }

        ~ScorePerEnemiesCounter() =>
            _killer.Killed -= OnKilled;

        private void OnKilled(IDamageable obj)
        {
            Score += (int)(((_complexity.GetComplexity(_enemiesSpawner.CurrentWave) * 0.1f + 1) * _scorePerKill) * (_enemiesSpawner.CurrentWave * 0.1f + 1));

            ScoreUpdated?.Invoke();
        }
    }
}
