using Clones.GameLogic;
using UnityEngine;

namespace Clones.UI
{
    public class RevivalButton : MonoBehaviour
    {
        [SerializeField] private GameOverView _gameoverView;
        [SerializeField] private GameObject _button;

        private PlayerRevival _playerRevival;

        public void Init(PlayerRevival playerRevival)
        {
            _playerRevival = playerRevival;
        }

        public void Revival()
        {
            _playerRevival.TryRevive(_gameoverView.Close);

            if (_playerRevival?.CanRivival == false)
                Destroy(_button);
        }
    }
}
