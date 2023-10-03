using Clones.Services;
using Clones.UI;

namespace Clones.GameLogic
{
    public class PlayerDeath : IDisable
    {
        private readonly RevivalView _revivalView;
        private readonly PlayerHealth _player;
        private readonly ITimeScale _timeScale;
        private readonly EnemiesSpawner _enemySpawner;

        public PlayerDeath(RevivalView revivalView, PlayerHealth player, ITimeScale timeScale, EnemiesSpawner enemiesSpawner)
        {
            _revivalView = revivalView;
            _player = player;
            _timeScale = timeScale;
            _enemySpawner = enemiesSpawner;

            _player.Died += OnDied;
        }

        public void OnDisable() => 
            _player.Died -= OnDied;

        private void OnDied(IDamageable obj)
        {
            _timeScale.Scaled(0);
            _enemySpawner.DestroyExistingEnemies();
            _revivalView.Open();
        }
    }
}
