using System;
using System.Collections.Generic;

namespace Clones.Services
{
    public interface ILeaderboard : IService
    {
        IReadOnlyList<LeaderboardPlayer> LeaderboardPlayers { get; }
        int UserRank { get; }

        void Fill(Action callback = null);
        void SetPlayerScore(int score);
    }
}