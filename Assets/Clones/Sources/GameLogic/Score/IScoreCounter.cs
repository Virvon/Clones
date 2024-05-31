using System;

namespace Clones.GameLogic
{
    public interface IScoreCounter
    {
        event Action ScoreUpdated;
        int Score { get; }
    }
}
