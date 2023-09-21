using UnityEngine;
using Agava.YandexGames;
using System;

namespace Clones.GameLogic
{
    public class PlayerRevival
    {
        private const int RestoredHealthPercentage = 60;
        private const int MaxRevivivalsCount = 1;

        private PlayerHealth _player;

        public bool CanRivival => _revivivalsCount < MaxRevivivalsCount;

        private int _revivivalsCount = 0;

        public PlayerRevival(PlayerHealth player)
        {
            _player = player;
        }

        public void TryRevive(Action callback = null)
        {
            if (_revivivalsCount + 1 > MaxRevivivalsCount)
                return;

            _revivivalsCount++;

#if !UNITY_EDITOR && UNITY_WEBGL
        VideoAd.Show(onRewardedCallback: () =>
        {
            _revivivalsCount++;

            _player.Reborn((int)((RestoredHealthPercentage / 100f) * _player.MaxHealth));

            callback?.Invoke();
        });
#else
            _player.Reborn((int)((RestoredHealthPercentage / 100f) * _player.MaxHealth));

            callback?.Invoke();
#endif
        }
    }
}
