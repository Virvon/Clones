using System;

namespace Clones.GameLogic
{
    public class ScorePerQuestsCounter : IScoreCounter
    {
        private readonly IQuestsCreator _questsCreator;
        private readonly EnemiesSpawner _enemiesSpawner;
        private readonly Complexity _complexity;
        private readonly int _scorePerQuest;

        public ScorePerQuestsCounter(IQuestsCreator questsCreator, EnemiesSpawner enemiesSpawner, Complexity complexity, int scorePerQuest)
        {
            _questsCreator = questsCreator;
            _enemiesSpawner = enemiesSpawner;
            _complexity = complexity;
            _scorePerQuest = scorePerQuest;

            _questsCreator.Completed += OnQuestCompleted;
        }

        ~ScorePerQuestsCounter() =>
            _questsCreator.Completed -= OnQuestCompleted;

        public event Action ScoreUpdated;

        public int Score { get; private set; }

        private void OnQuestCompleted()
        {
            Score += (int)((_complexity.GetComplexity(_enemiesSpawner.CurrentWave) * 0.1f + 1) * _scorePerQuest * (_enemiesSpawner.CurrentWave * 0.1f + 1));

            ScoreUpdated?.Invoke();
        }
    }
}
