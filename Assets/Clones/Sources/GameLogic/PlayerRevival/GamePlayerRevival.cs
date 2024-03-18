using System;
using Clones.Services;

namespace Clones.GameLogic
{
    public class GamePlayerRevival : IPlayerRevival
    {
        private const int RestoredHealthPercentage = 60;
        private const int MaxRevivivalsCount = 1;

        private readonly PlayerHealth _player;
        private readonly ITimeScale _timeScale;
        private readonly IAdvertisingDisplay _advertising;

        public bool CanRivival => _revivivalsCount < MaxRevivivalsCount;

        private int _revivivalsCount = 0;

        public GamePlayerRevival(PlayerHealth player, ITimeScale timeScale, IAdvertisingDisplay advertising)
        {
            _player = player;
            _timeScale = timeScale;
            _advertising = advertising;
        }

        public bool TryRevive(Action callback = null)
        {
            if (_revivivalsCount + 1 > MaxRevivivalsCount)
                return false;

            _advertising.ShowVideoAd(callback: () =>
            {
                _revivivalsCount++;
                _player.Reborn((int)((RestoredHealthPercentage / 100f) * _player.MaxHealth));

                callback?.Invoke();
            });

            return true;
        }
    }
}
