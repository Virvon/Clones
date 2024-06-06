using Clones.Auxiliary;
using Clones.GameLogic;
using TMPro;
using UnityEngine;

namespace Clones.UI
{
    public class ScoreCountPerGame : MonoBehaviour
    {
        private const int StartScore = 0;

        [SerializeField] private TMP_Text _scoreValue;

        private IMainScoreCounter _scoreCounter;

        public void Init(IMainScoreCounter scoreCounter)
        {
            _scoreCounter = scoreCounter;

            _scoreValue.text = StartScore.ToString();

            _scoreCounter.ScoreUpdated += OnScoreUpdated;
        }

        private void OnDestroy() => 
            _scoreCounter.ScoreUpdated -= OnScoreUpdated;

        private void OnScoreUpdated() => 
            _scoreValue.text = NumberFormatter.DivideIntegerOnDigits(_scoreCounter.Score);
    }
}
