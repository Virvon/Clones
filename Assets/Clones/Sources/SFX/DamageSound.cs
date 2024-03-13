using UnityEngine;

namespace Clones.SFX
{
    public class DamageSound : MonoBehaviour
    {
        [SerializeField] private PlayerHealth _playerHealth;
        [SerializeField] private AudioSource _audioSource;

        private void OnEnable() => 
            _playerHealth.DamageTaked += OnDamageTaked;

        private void OnDisable() => 
            _playerHealth.DamageTaked -= OnDamageTaked;

        private void OnDamageTaked() => 
            _audioSource.Play();
    }
}