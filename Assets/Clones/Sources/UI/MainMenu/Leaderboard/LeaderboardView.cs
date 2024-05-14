using Agava.YandexGames;
using Clones.Infrastructure;
using Clones.Services;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Clones.UI
{
    public class LeaderboardView : MonoBehaviour
    {
        [SerializeField] private GameObject _background;

        [SerializeField] private GameObject _authorizeView;
        [SerializeField] private Transform _container;
        [SerializeField] private TMP_Text _userScore;
        [SerializeField] private TMP_Text _userRank;

        private ILeaderboard _leaderboard;
        private IMainMenuFactory _mainMenuFactory;
        private IPersistentProgressService _persistentProgres;
        private List<LeaderboardElement> _spawnedElements;

        public void Init(ILeaderboard leaderboard, IMainMenuFactory mainMenuFactory, IPersistentProgressService persistentProgress)
        {
            _leaderboard = leaderboard;
            _mainMenuFactory = mainMenuFactory;
            _persistentProgres = persistentProgress;

            _spawnedElements = new();
        }

        public void Open()
        {
#if UNITY_WEBGL && !UNITY_EDITOR
            if (PlayerAccount.IsAuthorized)
                OpenLeaderboardView();
            else
                OpenAuthorizeView();
#else
            OpenAuthorizeView();
#endif
        }

        public void Close()
        {
            _background.SetActive(false);
            Clear();
        }

        private void OpenAuthorizeView() => 
            _authorizeView.SetActive(true);

        private void OpenLeaderboardView()
        {
            _leaderboard.Fill(callback: () =>
            {
                Construct();
                _background.SetActive(true);
            });
        }

        private void Construct()
        {
            Debug.Log("construct " + _leaderboard.LeaderboardPlayers.Count);

#if UNITY_WEBGL && !UNITY_EDITOR
            foreach (LeaderboardPlayer player in _leaderboard.LeaderboardPlayers)
            {
                LeaderboardElement leaderboardElement = _mainMenuFactory.CreateLeaderboardElement(player, _container);
                _spawnedElements.Add(leaderboardElement);
                Debug.Log(leaderboardElement.name);
            }

            _userRank.text = _leaderboard.UserRank.ToString();
#endif

            _userScore.text = _persistentProgres.Progress.AvailableClones.ScoreSum.ToString();
        }

        private void Clear()
        {
            foreach (LeaderboardElement element in _spawnedElements)
                Destroy(element);

            _spawnedElements = new();
        }
    }
}
