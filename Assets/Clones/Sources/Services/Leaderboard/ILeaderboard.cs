using System;
using System.Collections.Generic;

namespace Clones.Services
{
    public interface ILeaderboard : IService
    {
        IReadOnlyList<LeaderboardPlayer> LeaderboardPlayers { get; }

        void Fill();
        void SetPlayerScore(int score);
    }
}