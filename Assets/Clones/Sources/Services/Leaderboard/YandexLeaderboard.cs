﻿using Agava.YandexGames;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Clones.Services
{
    public class YandexLeaderboard : ILeaderboard
    {
        private const string LeaderboardName = "Leaderboard";
        private const string AnonymousName = "Anonymous";

        private readonly List<LeaderboardPlayer> _leaderboardPlayers;

        public IReadOnlyList<LeaderboardPlayer> LeaderboardPlayers => _leaderboardPlayers;

        public YandexLeaderboard()
        {
            _leaderboardPlayers = new();
        }

        public void SetPlayerScore(int score)
        {
            if (PlayerAccount.IsAuthorized == false)
                return;

            Leaderboard.GetEntries(LeaderboardName, (result) =>
            {
                Leaderboard.SetScore(LeaderboardName, score);
            });
        }

        public void Fill()
        {
#if UNITY_WEBGL && !UNITY_EDITOR
            if (PlayerAccount.IsAuthorized == false)
                return;

            _leaderboardPlayers.Clear();

            Leaderboard.GetEntries(LeaderboardName, (result) =>
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
            });
#endif
        }
    }
}
