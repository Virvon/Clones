using UnityEngine;

namespace Clones.UI
{
    public class RevivalButton : MonoBehaviour
    {
        [SerializeField] private PlayerRevival _playerRevival;
        [SerializeField] private GameOverView _gameoverView;

        private void OnEnable()
        {
            if(_playerRevival.CanRivival == false)
                Destroy(gameObject);
        }

        public void Revival()
        {
            _playerRevival.TryRevive(() =>
            {
                _gameoverView.Close();
            });
        }
    }
}
