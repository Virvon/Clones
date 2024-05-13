using UnityEngine;

namespace Clones.UI
{
    public class LeaderboardToggle : MonoBehaviour
    {
        [SerializeField] private LeaderboardView _leaderboard;

        public void Open() =>
            _leaderboard.Open();

        public void Close() =>
            _leaderboard.Close();
    }
}