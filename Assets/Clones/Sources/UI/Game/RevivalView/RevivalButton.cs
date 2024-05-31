using Clones.GameLogic;
using UnityEngine;

namespace Clones.UI
{
    public class RevivalButton : MonoBehaviour
    {
        [SerializeField] private GameRevivalView _revivalView;

        private IPlayerRevival _playerRevival;
        private GameOverView _gameOverView;

        public void Init(IPlayerRevival playerRevival, GameOverView gameOverView)
        {
            _playerRevival = playerRevival;
            _gameOverView = gameOverView;
        }

        public void Revival()
        {
            _playerRevival.TryRevive(successCallback: () => _revivalView.Close(), failureCallback: () => _revivalView.Close(_gameOverView.Open));
            _revivalView.StopTimer();
        }
    }
}
