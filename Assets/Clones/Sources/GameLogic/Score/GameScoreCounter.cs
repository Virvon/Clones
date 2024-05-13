using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Clones.GameLogic
{
    public class GameScoreCounter : IMainScoreCounter
    {
        private List<IScoreCounter> _scoreCounters;

        public int Score => _scoreCounters.Sum(scoreCounter => scoreCounter.Score);

        public GameScoreCounter() => 
            _scoreCounters = new();

        public void Add(IScoreCounter scoreCounter) => 
            _scoreCounters.Add(scoreCounter);

        public void Clear() => 
            _scoreCounters.Clear();

        public void ShowInfo()
        {
            Debug.Log("main counter " + _scoreCounters.Count);

            foreach (var scorecounter in _scoreCounters)
                scorecounter.ShowInfo();
        }
    }
}
