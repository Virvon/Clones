using Clones.Character;
using Clones.Environment;
using System;
using UnityEngine;

namespace Clones.SFX
{
    public class PreyResourceDieSound : MonoBehaviour
    {
        [SerializeField] private PreyResource _preyResource;
        [SerializeField] private AudioSource _audioSource;

        public void Init(AudioClip audioClip, float volume)
        {
            _audioSource.clip = audioClip;
            _audioSource.volume = volume;

            _preyResource.Died += OnDied;
        }

        private void OnDestroy() => 
            _preyResource.Died -= OnDied;

        private void OnDied(IDamageable obj) => 
            _audioSource.Play();
    }
}