using UnityEngine;

namespace Clones.SFX
{
    public class ShootSound : MonoBehaviour
    {
        [SerializeField] private AudioSource _audioSource;
        [SerializeField] private CharacterAttack _characterAttack;

        protected AudioSource AudioSource => _audioSource;

        private void OnEnable() => 
            _characterAttack.Attacked += OnAttacked;


        private void OnDisable() => 
            _characterAttack.Attacked -= OnAttacked;

        private void OnAttacked() => 
            _audioSource.Play();
    }
}
