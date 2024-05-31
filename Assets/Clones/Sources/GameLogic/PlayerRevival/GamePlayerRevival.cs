using System;
using Clones.Character.Player;
using Clones.Services;

namespace Clones.GameLogic
{
    public class GamePlayerRevival : IPlayerRevival
    {
        private const int RestoredHealthPercentage = 60;
        private const int MaxRevivivalsCount = 1;

        private readonly PlayerHealth _player;
        private readonly IAdvertisingDisplay _advertising;
        private int _revivivalsCount = 0;

        public bool CanRivival => _revivivalsCount < MaxRevivivalsCount;

        public GamePlayerRevival(PlayerHealth player, IAdvertisingDisplay advertising)
        {
            _player = player;
            _advertising = advertising;
        }

        public bool TryRevive(Action successCallback = null, Action failureCallback = null)
        {
            if (_revivivalsCount + 1 > MaxRevivivalsCount)
                return false;

            _advertising.ShowVideoAd(rewardedCallback: () =>
            {
                _revivivalsCount++;
                _player.Reborn((int)((RestoredHealthPercentage / 100f) * _player.MaxHealth));

                successCallback?.Invoke();
            },
            errorCallback: () => failureCallback?.Invoke());

            return true;
        }
    }
}