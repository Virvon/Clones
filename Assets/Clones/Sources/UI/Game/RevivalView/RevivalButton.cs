using Clones.GameLogic;
using UnityEngine;

namespace Clones.UI
{
    public class RevivalButton : MonoBehaviour
    {
        [SerializeField] private GameRevivalView _revivalView;

        private GamePlayerRevival _playerRevival;

        public void Init(GamePlayerRevival playerRevival)
        {
            _playerRevival = playerRevival;
        }

        public void Revival()
        {
            _playerRevival.TryRevive(_revivalView.Close);
            _revivalView.StopTimer();
        }
    }
}
