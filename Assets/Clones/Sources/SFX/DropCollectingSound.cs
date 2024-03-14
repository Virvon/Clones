using UnityEngine;

namespace Clones.SFX
{
    public class DropCollectingSound : MonoBehaviour
    {
        [SerializeField] private DropCollecting _dropCollecting;
        [SerializeField] private AudioSource _audioSource;

        private void OnEnable() => 
            _dropCollecting.Collected += OnCollected;

        private void OnDisable() =>
            _dropCollecting.Collected -= OnCollected;

        private void OnCollected() =>
            _audioSource.Play();
    }
}
