using System;
using System.Collections.Generic;

namespace Clones.Services
{
    public interface ILeaderboard : IService
    {
        IReadOnlyList<LeaderboardPlayer> LeaderboardPlayers { get; }

        void Fill(Action callback = null);
        void SetPlayerScore(int score);
    }
}