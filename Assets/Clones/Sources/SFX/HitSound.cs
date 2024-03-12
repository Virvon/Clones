using UnityEngine;

namespace Clones.SFX
{
    public class HitSound : MonoBehaviour
    {
        [SerializeField] private AudioSource _audioSource;
        [SerializeField] private Bullet _bullet;

        private void OnEnable()
        {
            _bullet.Hitted += OnHitted;
        }

        private void OnDisable()
        {
            _bullet.Hitted -= OnHitted;
        }

        private void OnHitted()
        {
            _audioSource.Play();
        }
    }
}