using System;

namespace Clones.GameLogic
{
    public class ScoreCounter : IScoreCounter
    {
        private readonly IScoreable _scoreable;
        private readonly EnemiesSpawner _enemiesSpawner;
        private readonly Complexity _complexity;
        private readonly int _scoreReward;

        public ScoreCounter(IScoreable scoreable, EnemiesSpawner enemiesSpawner, Complexity complexity, int scoreReward)
        {
            _scoreable = scoreable;
            _enemiesSpawner = enemiesSpawner;
            _complexity = complexity;
            _scoreReward = scoreReward;

            _scoreable.Scored += OnScored;
        }

        ~ScoreCounter() =>
            _scoreable.Scored -= OnScored;

        public event Action ScoreUpdated;

        public int Score { get; private set; }


        private void OnScored()
        {
            Score += (int)((_complexity.GetComplexity(_enemiesSpawner.CurrentWave) * 0.1f + 1) * _scoreReward * (_enemiesSpawner.CurrentWave * 0.1f + 1));

            ScoreUpdated?.Invoke();
        }
    }
}
