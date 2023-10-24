namespace Clones.GameLogic
{
    public class AttackShake : IDisable
    {
        private const float _amplitudeGain =1;
        private const float _frequencyGain = 1;
        private const float _delay = 0.2f;

        private readonly CharacterAttack _characterAttack;
        private readonly CameraShake _cameraShake;

        public AttackShake(CharacterAttack characterAttack, CameraShake cameraShake)
        {
            _characterAttack = characterAttack;
            _cameraShake = cameraShake;

            _characterAttack.Attacked += OnAttacked;
        }

        public void OnDisable() =>
            _characterAttack.Attacked -= OnAttacked;

        private void OnAttacked() => 
            _cameraShake.Shake(_amplitudeGain, _frequencyGain, _delay);
    }
}
