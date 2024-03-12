using UnityEngine;

namespace Clones.SFX
{
    public class ShootSound : MonoBehaviour
    {
        [SerializeField] private AudioSource _audioSource;
        [SerializeField] private CharacterAttack _characterAttack;

        public void Init(AudioClip audioClip, float volume)
        {
            _audioSource.clip = audioClip;
            _audioSource.volume = volume;
            _characterAttack.Attacked += OnAttacked;
        }

        private void OnDestroy() => 
            _characterAttack.Attacked -= OnAttacked;

        private void OnAttacked() => 
            _audioSource.Play();
    }
}
