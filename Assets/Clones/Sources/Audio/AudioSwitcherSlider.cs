using Clones.Data;
using Clones.Services;
using System;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

namespace Clones.Audio
{
    public abstract class AudioSwitcherSlider : MonoBehaviour, IProgressReader
    {
        private const int MinSoundVolume = -25;
        private const int SoundOffVolume = -80;
        private const int SoundOnVolume = 0;

        [SerializeField] private string _mixerName;
        [SerializeField] private AudioMixerGroup _mixer;
        [SerializeField] private Slider _audioSlider;

        public event Action AudioVolumeChanged;

        public bool IsAudioActive => CheckAudioActive();

        protected IPersistentProgressService Progress { get; private set; }

        public void Init(IPersistentProgressService progress) =>
            Progress = progress;

        public void SetAudioVolume()
        {
            int volume = _audioSlider.value > MinSoundVolume ? (int)_audioSlider.value : SoundOffVolume;

            _mixer.audioMixer.SetFloat(_mixerName, volume);
            SetProgress(volume);

            AudioVolumeChanged?.Invoke();
        }

        public void SetAudioVolume(int volume)
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

            int volume = isActive ? SoundOnVolume : SoundOffVolume;

            _mixer.audioMixer.SetFloat(_mixerName, volume);
            _audioSlider.value = volume;
            SetProgress(volume);

            AudioVolumeChanged?.Invoke();
        }

        public void UpdateProgress() => 
            SetAudioVolume();

        protected abstract void SetProgress(int volume);

        private bool CheckAudioActive()
        {
            _mixer.audioMixer.GetFloat(_mixerName, out float value);

            return value > MinSoundVolume;
        }
    }
}