using UnityEngine;

namespace Clones.CameraShake
{
    public class AttackShake : MonoBehaviour
    {
        [SerializeField] private CharacterAttack _characterAttack;
        [SerializeField] private CameraShake _cameraShake;

        [SerializeField] private float _amplitudeGain;
        [SerializeField] private float _frequencyGain;
        [SerializeField] private float _delay;

        private void OnEnable() => _characterAttack.Attacked += OnAttacked;

        private void OnDisable() => _characterAttack.Attacked -= OnAttacked;
            
        private void OnAttacked() => _cameraShake.Shake(_amplitudeGain, _frequencyGain, _delay);
    }
}
