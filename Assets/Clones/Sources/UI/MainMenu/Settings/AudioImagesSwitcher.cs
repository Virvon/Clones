using Clones.Audio;
using UnityEngine;
using UnityEngine.UI;

namespace Clones.UI
{
    public class AudioImagesSwitcher : MonoBehaviour
    {
        [SerializeField] private Image _activeImage;
        [SerializeField] private Image _deactiveImage;
        [SerializeField] private AudioSwitcherSlider _audioSwitcher;

        private Image _currentImage;

        private void OnEnable() => 
            _audioSwitcher.AudioVolumeChanged += OnAudioVolumeChanged;

        private void Start() => 
            OnAudioVolumeChanged();

        private void OnDisable() => 
            _audioSwitcher.AudioVolumeChanged -= OnAudioVolumeChanged;

        private void OnAudioVolumeChanged()
        {
            if (_audioSwitcher.IsAudioActive)
                EnebleImage(_activeImage);
            else
                EnebleImage(_deactiveImage);
        }

        private void EnebleImage(Image image)
        {
            _currentImage?.gameObject.SetActive(false);
            _currentImage = image;
            _currentImage.gameObject.SetActive(true);
        }
    }
}