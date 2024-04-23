using Clones.Services;
using System;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

namespace Clones.Audio
{
    public abstract class AudioSwitcher : MonoBehaviour
    {
        private const int MinSoundVolume = -25;
        private const int SoundOffVolume = -80;
        private const int SoundOnVolume = 0;

        [SerializeField] private string _mixerName;
        [SerializeField] private AudioMixerGroup _mixer;
        [SerializeField] private Slider _audioSlider;  

        protected IPersistentProgressService Progress { get; private set; }

        public bool IsAudioActive => CheckAudioActive();

        public event Action AudioVolumeChanged;

        public void Init(IPersistentProgressService progress) =>
            Progress = progress;

        public void SetAudioVolume()
        {
            float volume = _audioSlider.value > MinSoundVolume ? _audioSlider.value : SoundOffVolume;

            _mixer.audioMixer.SetFloat(_mixerName, volume);
            SetProgress(volume);

            AudioVolumeChanged?.Invoke();
        }

        public void SetAudioVolume(float volume)
        {
            volume = volume > MinSoundVolume ? volume : SoundOffVolume;

            _audioSlider.value = volume;
            _mixer.audioMixer.SetFloat(_mixerName, volume);
            SetProgress(volume);

            AudioVolumeChanged?.Invoke();
        }

        public void SetActiveSound(bool isActive)
        {
            if (isActive == IsAudioActive)
                return;

            float volume = isActive ? SoundOnVolume : SoundOffVolume;

            _mixer.audioMixer.SetFloat(_mixerName, volume);
            _audioSlider.value = volume;
            SetProgress(volume);

            AudioVolumeChanged?.Invoke();
        }

        protected abstract void SetProgress(float volume);

        private bool CheckAudioActive()
        {
            _mixer.audioMixer.GetFloat(_mixerName, out float value);

            return value > MinSoundVolume;
        }
    }
}