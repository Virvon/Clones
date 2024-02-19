using UnityEngine;
using Agava.YandexGames;
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

        public bool CanRivival => _revivivalsCount < MaxRevivivalsCount;

        private int _revivivalsCount = 0;

        public GamePlayerRevival(PlayerHealth player, ITimeScale timeScale)
        {
            _player = player;
            _timeScale = timeScale;
        }

        public bool TryRevive(Action callback = null)
        {
            if (_revivivalsCount + 1 > MaxRevivivalsCount)
                return false;

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
            _timeScale.Scaled(1);

            callback?.Invoke();
#endif

            return true;
        }
    }
}
