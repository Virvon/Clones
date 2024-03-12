using UnityEngine;
using UnityEngine.UI;

namespace Clones.SFX
{
    public class ButtonClickSound : MonoBehaviour
    {
        [SerializeField] private AudioSource _audioSource;
        [SerializeField] private Button _button;

        private void OnEnable() => 
            _button.onClick.AddListener(OnClick);

        private void OnDisable() => 
            _button.onClick.RemoveListener(OnClick);

        private void OnClick() => 
            _audioSource.Play();
    }
}