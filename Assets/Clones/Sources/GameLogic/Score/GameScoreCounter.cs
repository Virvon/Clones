using System;
using System.Collections.Generic;
using System.Linq;

namespace Clones.GameLogic
{
    public class GameScoreCounter : IMainScoreCounter
    {
        private List<IScoreCounter> _scoreCounters;

        public int Score => _scoreCounters.Sum(scoreCounter => scoreCounter.Score);

        public event Action ScoreUpdated;

        public GameScoreCounter() => 
            _scoreCounters = new();

        public void Add(IScoreCounter scoreCounter)
        {
            _scoreCounters.Add(scoreCounter);
            scoreCounter.ScoreUpdated += ()=> ScoreUpdated?.Invoke();
        }

        public void Clear()
        {
            foreach(IScoreCounter scoreCounter in _scoreCounters)
                scoreCounter.ScoreUpdated -= () => ScoreUpdated?.Invoke();

            _scoreCounters.Clear();
        }
    }
}
