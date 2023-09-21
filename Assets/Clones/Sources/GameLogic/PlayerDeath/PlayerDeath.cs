using Clones.UI;

namespace Clones.GameLogic
{
    public class PlayerDeath : IDisable
    {
        private readonly GameOverView _gameOverView;
        private readonly PlayerHealth _player;

        public PlayerDeath(GameOverView gameOverView, PlayerHealth player)
        {
            _gameOverView = gameOverView;
            _player = player;

            _player.Died += OnDied;
        }

        public void OnDisable() => 
            _player.Died += OnDied;

        private void OnDied(IDamageable obj)
        {
            _gameOverView.Open();
        }
    }
}
