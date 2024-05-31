using Clones.Environment;
using UnityEngine;

namespace Clones.SFX
{
    public class PreyResourceDamageSound : MonoBehaviour
    {
        [SerializeField] private PreyResource _preyResource;
        [SerializeField] private AudioSource _audioSource;

        public void Init(AudioClip audioClip, float volume)
        {
            _audioSource.clip = audioClip;
            _audioSource.volume = volume;

            _preyResource.Damaged += OnDamaged;
        }

        private void OnDestroy() =>
            _preyResource.Damaged -= OnDamaged;

        private void OnDamaged() => 
            _audioSource.Play();
    }
}