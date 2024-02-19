using Clones.Services;
using Clones.UI;
using UnityEngine;

namespace Clones.GameLogic
{
    public class PlayerDeath : IDisable
    {
        private readonly GameRevivalView _revivalView;
        private readonly PlayerHealth _player;
        private readonly ITimeScale _timeScale;
        private readonly EnemiesSpawner _enemySpawner;
        private readonly GameTimer _gameTimer;

        public PlayerDeath(GameRevivalView revivalView, PlayerHealth player, ITimeScale timeScale, EnemiesSpawner enemiesSpawner, GameTimer gameTimer)
        {
            _revivalView = revivalView;
            _player = player;
            _timeScale = timeScale;
            _enemySpawner = enemiesSpawner;
            _gameTimer = gameTimer;

            _player.Died += OnDied;
        }

        public void OnDisable() => 
            _player.Died -= OnDied;

        private void OnDied(IDamageable obj)
        {
            Debug.Log(_gameTimer.Stop());
            _timeScale.Scaled(0);
            _enemySpawner.DestroyExistingEnemies();
            _revivalView.Open();
        }
    }
}
