using Agava.WebUtility;
using UnityEngine.Audio;

namespace Clones.Services
{
    public class ActivityTracking : IService
    {
        private const string MasterMixer = "MasterVolume";
        private const int NormalSoundVolume = 0;
        private const int MutedSoundVolume = -80;
        private const int NormalTimeScale = 1;
        private const int StoppedTimeScale = 0;

        private readonly ITimeScale _timeScale;
        private readonly AudioMixerGroup _audioMixer;

        public ActivityTracking(ITimeScale timeScale, AudioMixerGroup audioMixer)
        {
            _timeScale = timeScale;
            _audioMixer = audioMixer;

            WebApplication.InBackgroundChangeEvent += OnInBackgroundChange;
        }

        ~ActivityTracking() =>
            WebApplication.InBackgroundChangeEvent -= OnInBackgroundChange;

        private void OnInBackgroundChange(bool inBackground)
        {
            _timeScale.Scaled(inBackground ? NormalTimeScale : StoppedTimeScale);
            _audioMixer.audioMixer.SetFloat(MasterMixer, inBackground ? NormalSoundVolume : MutedSoundVolume);
        }
    }
}