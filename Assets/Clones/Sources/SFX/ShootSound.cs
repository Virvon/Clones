using UnityEngine;

namespace Clones.SFX
{
    public class ShootSound : MonoBehaviour
    {
        [SerializeField] private AudioSource _audioSource;
        [SerializeField] private CharacterAttack _characterAttack;

        protected AudioSource AudioSource => _audioSource;

        private void OnEnable() => 
            _characterAttack.AttackCompleted += OnAttacked;


        private void OnDisable() => 
            _characterAttack.AttackCompleted -= OnAttacked;

        private void OnAttacked() => 
            _audioSource.Play();
    }
}
