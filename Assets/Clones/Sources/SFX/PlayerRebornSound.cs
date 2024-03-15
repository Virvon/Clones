using UnityEngine;

namespace Clones.SFX
{
    public class PlayerRebornSound : MonoBehaviour
    {
        [SerializeField] private PlayerHealth _playerHealth;
        [SerializeField] private AudioSource _audioSource;

        private void OnEnable() => 
            _playerHealth.Reborned += OnReborned;

        private void OnDisable() => 
            _playerHealth.Reborned -= OnReborned;

        private void OnReborned() => 
            _audioSource.Play();
    }
}
