using Clones.GameLogic;
using UnityEngine;

namespace Clones.UI
{
    public class RevivalButton : MonoBehaviour
    {
        [SerializeField] private RevivalView _revivalView;

        private PlayerRevival _playerRevival;

        public void Init(PlayerRevival playerRevival)
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
