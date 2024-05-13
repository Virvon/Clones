using Agava.YandexGames;
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
#if UNITY_WEBGL && !UNITY_EDITOR
            if (PlayerAccount.IsAuthorized == false)
                return;

            Leaderboard.GetEntries(LeaderboardName, onSuccessCallback: (result) =>
            {
                Debug.Log("sucsess set player score " + score);
                Leaderboard.SetScore(LeaderboardName, score);
            }, onErrorCallback: (value) => Debug.Log("error set player score " + value));
#endif
        }

        public void Fill(Action callback = null)
        {
#if UNITY_WEBGL && !UNITY_EDITOR
            if (PlayerAccount.IsAuthorized == false)
                return;

            _leaderboardPlayers.Clear();
            Debug.Log("Fill clear " + _leaderboardPlayers.Count);

            Leaderboard.GetEntries(LeaderboardName, (result) =>
            {
                Debug.Log("success fill");

                foreach (LeaderboardEntryResponse entry in result.entries)
                {
                    int rank = entry.rank;
                    int score = entry.score;
                    string name = entry.player.publicName;

                    if (string.IsNullOrEmpty(name))
                        name = AnonymousName;

                    _leaderboardPlayers.Add(new LeaderboardPlayer(rank, name, score));
                }

                callback?.Invoke();
            }, onErrorCallback: (value) => Debug.Log("error fill " + value));
#else
            callback?.Invoke();
#endif
        }
    }
}
