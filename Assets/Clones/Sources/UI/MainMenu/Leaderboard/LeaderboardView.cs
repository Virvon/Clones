using Clones.Infrastructure;
using Clones.Services;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Clones.UI
{
    public class LeaderboardView : MonoBehaviour
    {
        [SerializeField] private Transform _container;
        [SerializeField] private TMP_Text _scoreValue;

        private ILeaderboard _leaderboard;
        private IMainMenuFactory _mainMenuFactory;
        private IPersistentProgressService _persistentProgres;

        public void Init(ILeaderboard leaderboard, IMainMenuFactory mainMenuFactory, IPersistentProgressService persistentProgress)
        {
            _leaderboard = leaderboard;
            _mainMenuFactory = mainMenuFactory;
            _persistentProgres = persistentProgress;

            Construct();
        }

        private void Construct()
        {
            Debug.Log("construct " + _leaderboard.LeaderboardPlayers.Count);

            _scoreValue.text = _persistentProgres.Progress.AvailableClones.ScoreSum.ToString();

#if UNITY_WEBGL && !UNITY_EDITOR
            foreach(LeaderboardPlayer player in _leaderboard.LeaderboardPlayers)
            {
                LeaderboardElement leaderboardElement = _mainMenuFactory.CreateLeaderboardElement(player, _container);
                Debug.Log(leaderboardElement.name);
            }
#endif
        }
    }
}
