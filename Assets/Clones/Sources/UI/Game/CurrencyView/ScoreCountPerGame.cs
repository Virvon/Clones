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

            Debug.Log(scoreCounter != null);
        }

        private void OnDestroy() => 
            _scoreCounter.ScoreUpdated -= OnScoreUpdated;

        private void OnScoreUpdated()
        {
            Debug.Log("score update");
            _scoreValue.text = _scoreCounter.Score.ToString();
        }
    }
}
