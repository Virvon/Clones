namespace Clones.GameLogic
{
    public class AttackShake : IDisabled
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

            _characterAttack.AttackCompleted += OnAttacked;
        }

        public void Disable() =>
            _characterAttack.AttackCompleted -= OnAttacked;

        private void OnAttacked() => 
            _cameraShake.Shake(_amplitudeGain, _frequencyGain, _delay);
    }
}
