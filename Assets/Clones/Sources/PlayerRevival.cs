using UnityEngine;
using Agava.YandexGames;
using System;

public class PlayerRevival : MonoBehaviour
{
    //[SerializeField] private Player _player;
    [SerializeField, Range(0, 100)] int _restoredHealthPercentage;
    [SerializeField] private int _maxRevivivalsCount;

    public bool CanRivival => _revivivalsCount < _maxRevivivalsCount;

    private int _revivivalsCount = 0;

    public void TryRevive(Action callback = null)
    {
        if (_revivivalsCount + 1 > _maxRevivivalsCount)
            return;

#if !UNITY_EDITOR && UNITY_WEBGL
        VideoAd.Show(onRewardedCallback: () =>
        {
            _revivivalsCount++;

            _player.Reborn((int)((_restoredHealthPercentage / 100) * _player.MaxHealth));

            callback?.Invoke();
        });
#else
        _revivivalsCount++;

        //_player.Reborn((int)((_restoredHealthPercentage / 100) * _player.MaxHealth));

        callback?.Invoke();
#endif
    }
}
