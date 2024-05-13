using UnityEngine;

namespace Clones.GameLogic
{
    public class ScorePerEnemiesCounter : IScoreCounter
    {
        private readonly IKiller _killer;
        private readonly EnemiesSpawner _enemiesSpawner;
        private readonly int _scorePerKill;

        public int Score { get; private set; }

        public ScorePerEnemiesCounter(IKiller killer, EnemiesSpawner enemiesSpawner, int scorePerKill)
        {
            _killer = killer;
            _enemiesSpawner = enemiesSpawner;
            _scorePerKill = scorePerKill;

            _killer.Killed += OnKilled;
        }

        ~ScorePerEnemiesCounter() =>
            _killer.Killed -= OnKilled;

        private void OnKilled(IDamageable obj) => 
            Score += _enemiesSpawner.CurrentWave * _scorePerKill;

        public void ShowInfo()
        {
            Debug.Log("счет за убийства " + Score);
        }
    }
}
