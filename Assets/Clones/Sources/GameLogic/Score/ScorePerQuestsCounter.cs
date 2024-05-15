using System;

namespace Clones.GameLogic
{
    public class ScorePerQuestsCounter : IScoreCounter
    {
        private readonly IQuestsCreator _questsCreator;
        private readonly int _scorePerQuest;

        public int Score { get; private set; }

        public event Action ScoreUpdated;

        public ScorePerQuestsCounter(IQuestsCreator questsCreator, int scorePerQuest)
        {
            _questsCreator = questsCreator;
            _scorePerQuest = scorePerQuest;

            _questsCreator.Completed += OnQuestCompleted;
        }

        ~ScorePerQuestsCounter() =>
            _questsCreator.Completed -= OnQuestCompleted;

        private void OnQuestCompleted()
        {
            Score += (int)(_questsCreator.Complexity * _scorePerQuest);

            ScoreUpdated?.Invoke();
        }
    }
}
