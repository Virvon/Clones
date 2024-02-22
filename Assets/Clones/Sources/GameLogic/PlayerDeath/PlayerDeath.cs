using Clones.Services;
using Clones.UI;
using System;

namespace Clones.GameLogic
{
    public class PlayerDeath : IDisable
    {
        private readonly IOpenableView _openableView;
        private readonly PlayerHealth _player;
        private readonly ITimeScale _timeScale;
        private readonly IDestoryableEnemies _destroyableEnemies;
        private readonly Action _callbakc;

        public PlayerDeath(IOpenableView openableView, PlayerHealth player, ITimeScale timeScale, IDestoryableEnemies destroyableEnemies, Action callback = null)
        {
            _openableView = openableView;
            _player = player;
            _timeScale = timeScale;
            _destroyableEnemies = destroyableEnemies;
            _callbakc = callback;

            _player.Died += OnDied;
        }

        public void OnDisable() => 
            _player.Died -= OnDied;

        private void OnDied(IDamageable obj)
        {
            _callbakc?.Invoke();
            _timeScale.Scaled(0);
            _destroyableEnemies.DestroyExistingEnemies();
            _openableView.Open();
        }
    }
}
