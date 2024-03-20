using Agava.YandexGames;
using System;
using UnityEngine;
using UnityEngine.Audio;

namespace Clones.Services
{
    public class AdvertisingDisplay : IAdvertisingDisplay
    {
        private const string MasterMixer = "MasterVolume";
        private const float SoundOffVolume = -80;
        private const int StoppedTimeScaleValue = 0;
        private const int NormalTimeScaleValue = 1;

        private readonly AudioMixerGroup _audioMixerGroup;
        private readonly ITimeScale _timeScale;
        private readonly ICoroutineRunner _runner;

        private float _volume;

        public AdvertisingDisplay(AudioMixerGroup audioMixerGroup, ITimeScale timeScale, ICoroutineRunner runner)
        {
            _audioMixerGroup = audioMixerGroup;
            _timeScale = timeScale;
            _runner = runner;
        }

        public void ShowVideoAd(Action callback)
        {
#if !UNITY_EDITOR && UNITY_WEBGL
            _timeScale.Scaled(StoppedTimeScaleValue);
            OffSound();

            VideoAd.Show(onRewardedCallback: ()=>
            {
                callback?.Invoke();
                _timeScale.Scaled(NormalTimeScaleValue);
                OnSound();
            });
#else
            callback?.Invoke();
#endif
        }

        public void ShowInterstitialAd(Action callback)
        {
#if !UNITY_EDITOR && UNITY_WEBGL
            _timeScale.Scaled(StoppedTimeScaleValue);
            OffSound();

            InterstitialAd.Show(onCloseCallback: (isClosed) =>
            {
                callback?.Invoke();
                _timeScale.Scaled(NormalTimeScaleValue);
                OnSound();
            });
#else
            callback?.Invoke();
#endif
        }

        private void OffSound()
        {
            _audioMixerGroup.audioMixer.GetFloat(MasterMixer, out _volume);
            _audioMixerGroup.audioMixer.SetFloat(MasterMixer, SoundOffVolume);
        }

        private void OnSound() =>
            _audioMixerGroup.audioMixer.SetFloat(MasterMixer, _volume);
    }
}
