using UnityEngine;
using UnityEngine.UI;

namespace Clones.SFX
{
    public class ButtonClickSound : MonoBehaviour
    {
        [SerializeField] private AudioSource _audioSource;
        [SerializeField] private Button _button;
        [SerializeField] private bool _hasButton = true;

        public void Init(Button button)
        {
            if (_hasButton)
            {
                Debug.LogError(nameof(ButtonClickSound) + "has button");
                return;
            }

            _button = button;
            _button.onClick.AddListener(OnClick);
        }

        private void OnEnable()
        {
            if(_hasButton)
                _button.onClick.AddListener(OnClick);
        }

        private void OnDisable()
        {
            if(_hasButton)
                _button.onClick.RemoveListener(OnClick);
        }

        private void OnDestroy()
        {
            if(_hasButton == false)
                _button.onClick.RemoveListener(OnClick);
        }

        private void OnClick() => 
            _audioSource.Play();
    }
}