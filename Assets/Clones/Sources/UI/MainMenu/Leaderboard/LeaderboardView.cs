using Clones.Infrastructure;
using Clones.Services;
using System.Collections.Generic;
using UnityEngine;

namespace Clones.UI
{
    public class LeaderboardView : MonoBehaviour
    {
        [SerializeField] private Transform _container;

        private ILeaderboard _leaderboard;
        private IMainMenuFactory _mainMenuFactory;

        public void Init(ILeaderboard leaderboard, IMainMenuFactory mainMenuFactory)
        {
            _leaderboard = leaderboard;
            _mainMenuFactory = mainMenuFactory;

            Construct();
        }

        private void Construct()
        {
            Debug.Log("construct " + _leaderboard.LeaderboardPlayers.Count);
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
