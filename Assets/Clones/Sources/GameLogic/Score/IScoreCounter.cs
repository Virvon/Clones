using System;

namespace Clones.GameLogic
{
    public interface IScoreCounter
    {
        int Score { get; }

        event Action ScoreUpdated;
    }
}
