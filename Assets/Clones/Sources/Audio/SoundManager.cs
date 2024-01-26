using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

namespace Clones.Audio
{
    public class SoundManager : MonoBehaviour
    {
        [SerializeField] private AudioMixerGroup _mixer;
        [SerializeField] private Slider _soundSlider;
        [SerializeField] private Slider _musicSlider;

        private string _masterMixerName = "MasterVolume";
        private string _SoundMixerName = "SoundVolume";
        private string _MusicMixerName = "MusicVolume";

        private float _currentValueMasterMixer;

        private const float SoundOnValue = 0;
        private const float SoundMinValue = -25;
        private const float SoundOffValue = -80;

        public void SetSoundValueMixer(float value)
        {
            _mixer.audioMixer.SetFloat(_masterMixerName, value);
        }

        public void SwitchOffSound()
        {
            SwitchMasterMixer(SoundOffValue, false, true);
        }

        public void SwitchOnSound()
        {
            SwitchMasterMixer(SoundOnValue, true, false);
        }

        public void SetVolumeSound()
        {
            _mixer.audioMixer.SetFloat(_SoundMixerName, GetVolume(_soundSlider.value));
        }

        public void SetVolumeMusic()
        {
            _mixer.audioMixer.SetFloat(_MusicMixerName, GetVolume(_musicSlider.value));
        }

        public float GetMixerValue()
        {
            return _currentValueMasterMixer;
        }

        private void SwitchMasterMixer(float soundValue, bool isActiveOffIcon, bool isActiveOnIcon)
        {
            _mixer.audioMixer.SetFloat(_masterMixerName, soundValue);
            _currentValueMasterMixer = soundValue;
        }

        private float GetVolume(float sliderValue)
        {
            if (sliderValue == 0)
                return SoundOffValue;

            return Mathf.Lerp(SoundMinValue, 0, sliderValue);
        }
    }
}