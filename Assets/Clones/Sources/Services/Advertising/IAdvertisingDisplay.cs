using System;

namespace Clones.Services
{
    public interface IAdvertisingDisplay : IService
    {
        void ShowInterstitialAd(Action callback);
        void ShowVideoAd(Action rewardedCallback, Action errorCallback);
    }
}