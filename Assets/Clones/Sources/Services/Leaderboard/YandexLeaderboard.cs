using Agava.YandexGames;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Clones.Services
{
    public class YandexLeaderboard : ILeaderboard
    {
        private const string LeaderboardName = "Leaderboard1";
        private const string AnonymousName = "Anonymous";

        private readonly List<LeaderboardPlayer> _leaderboardPlayers;

        public IReadOnlyList<LeaderboardPlayer> LeaderboardPlayers => _leaderboardPlayers;
        public int UserRank { get; private set; }

        public YandexLeaderboard() =>
            _leaderboardPlayers = new();

        public void SetPlayerScore(int score)
        {
#if UNITY_WEBGL && !UNITY_EDITOR
            if (PlayerAccount.IsAuthorized == false)
                return;

            Leaderboard.GetPlayerEntry(LeaderboardName, onSuccessCallback: (result) =>
            {
                if (result == null || result.score < score)
                    Leaderboard.SetScore(LeaderboardName, score);
            });
#endif
        }

        public void Fill(Action callback = null)
        {
#if UNITY_WEBGL && !UNITY_EDITOR
            if (PlayerAccount.IsAuthorized == false)
                return;
  
            _leaderboardPlayers.Clear();

            Leaderboard.GetEntries(LeaderboardName, onSuccessCallback: (result) =>
            {
                foreach (LeaderboardEntryResponse entry in result.entries)
                {
                    int rank = entry.rank;
                    int score = entry.score;
                    string name = entry.player.publicName;

                    if (string.IsNullOrEmpty(name))
                        name = AnonymousName;

                    _leaderboardPlayers.Add(new LeaderboardPlayer(rank, name, score));
                }

                UserRank = result.userRank;
              
                callback?.Invoke();
            });
#else
            callback?.Invoke();
#endif
        }
    }
}
