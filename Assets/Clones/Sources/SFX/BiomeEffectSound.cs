using UnityEngine;

namespace Clones.SFX
{
    public class BiomeEffectSound : MonoBehaviour
    {
        [SerializeField] private BiomeEffects _biomeEffects;
        [SerializeField] private AudioSource _audioSource;

        private void OnEnable() =>
            _biomeEffects.EffectStateChanged += OnEffectStateChanged;

        private void OnDisable() => 
            _biomeEffects.EffectStateChanged -= OnEffectStateChanged;

        private void OnEffectStateChanged()
        {
            if(_biomeEffects.EffectIsPlayed)
                _audioSource.Play();
            else
                _audioSource.Stop();
        }
    }
}
