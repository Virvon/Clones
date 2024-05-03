using Clones.Services;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

namespace Clones.Audio
{
    public class AudioSwitcherButton : MonoBehaviour
    {
        private const int MinSoundVolume = -25;
        private const int SoundOffVolume = -80;
        private const int SoundOnVolume = 0;

        [SerializeField] private Image _image;
        [SerializeField] private Button _button;
        [SerializeField] private AudioMixerGroup _audioMixer;
        [SerializeField] private string _musicMixerName;
        [SerializeField] private string _soundMixerName;
        [SerializeField] private Sprite _soundOnIcon;
        [SerializeField] private Sprite _soundOffIcon;

        private IPersistentProgressService _progress;

        private int _lastMusicVolume;
        private int _lastSoundVolume;

        private bool _isSoundOn => _progress.Progress.Settings.MusicVolume > MinSoundVolume || _progress.Progress.Settings.SoundVolume > MinSoundVolume;

        public void Init(IPersistentProgressService progress)
        {
            _progress = progress;

            ToggleIcon();
            SetLastVolume();

            _button.onClick.AddListener(OnButtonClick);
        }

        private void OnDestroy()
        {
            _button.onClick.RemoveListener(OnButtonClick);
        }

        private void OnButtonClick()
        {
            int musicVolume = _isSoundOn ? SoundOffVolume : _lastMusicVolume;
            int soundVolume = _isSoundOn ? SoundOffVolume : _lastSoundVolume;

            _audioMixer.audioMixer.SetFloat(_musicMixerName, musicVolume);
            _audioMixer.audioMixer.SetFloat(_soundMixerName, soundVolume);

            _progress.Progress.Settings.SoundVolume = soundVolume;
            _progress.Progress.Settings.MusicVolume = musicVolume;

            ToggleIcon();
        }

        private void ToggleIcon() =>
            _image.sprite = _isSoundOn ? _soundOnIcon : _soundOffIcon;

        private void SetLastVolume()
        {
            _lastSoundVolume = _isSoundOn ? _progress.Progress.Settings.SoundVolume : SoundOnVolume;
            _lastMusicVolume = _isSoundOn ? _progress.Progress.Settings.MusicVolume : SoundOnVolume;
        }
    }
}