using UnityEngine;

namespace Clones.Character
{
    public class DamageableDeath : MonoBehaviour
    {
        private GameObject _effect;
        private Vector3 _effectOffset;

        public void Init(GameObject effect, Vector3 effectOffset, IDamageable damageable)
        {
            _effect = effect;
            _effectOffset = effectOffset;

            damageable.Died += OnDied;
        }

        private void OnDied(IDamageable damageable)
        {
            damageable.Died -= OnDied;

            Instantiate(_effect, transform.position + _effectOffset, Quaternion.identity);
        }
    }
}