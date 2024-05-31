using System;

namespace Clones.GameLogic
{
    public class ScorePerItemsCounter : IScoreCounter
    {
        private readonly IItemsCounter _itemsCounter;
        private readonly EnemiesSpawner _enemiesSpawner;
        private readonly Complexity _complexity;
        private readonly int _scorePerItem;

        public ScorePerItemsCounter(IItemsCounter itemsCounter, EnemiesSpawner enemiesSpawner, Complexity complexity, int scorePerItem)
        {
            _itemsCounter = itemsCounter;
            _enemiesSpawner = enemiesSpawner;
            _complexity = complexity;
            _scorePerItem = scorePerItem;

            _itemsCounter.ItemTaked += OnItemTaked;
        }

        ~ScorePerItemsCounter() =>
            _itemsCounter.ItemTaked -= OnItemTaked;

        public event Action ScoreUpdated;

        public int Score { get; private set; }

        private void OnItemTaked()
        {
            Score += (int)((_complexity.GetComplexity(_enemiesSpawner.CurrentWave) * 0.1f + 1) * _scorePerItem * (_enemiesSpawner.CurrentWave * 0.1f + 1));

            ScoreUpdated?.Invoke();
        }
    }
}
