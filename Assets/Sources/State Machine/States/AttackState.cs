using UnityEngine;

namespace Clones.StateMachine
{
    public abstract class AttackState : State
    {
        [SerializeField] private Player _player;

        private readonly Collider[] _overlapColliders = new Collider[64];

        private float _attackRadius => _player.AttackRadius;
        private float _lookRotationSpeed => _player.LookRotationSpeed;
        private CharacterAttack _characterAttack => _player.CharacterAttack;

        private void Update() => Attack();

        private void Attack()
        {
            IDamageble target = GetNearTarget();

            if (target == null)
                return;

            _characterAttack.TryAttack(target);
            RotateTo(target.Position);
        }

        private void RotateTo(Vector3 target)
        {
            var direction = Quaternion.LookRotation(target - transform.position, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, direction, _lookRotationSpeed * Time.deltaTime);
        }

        private IDamageble GetNearTarget()
        {
            int overlapCount = Physics.OverlapSphereNonAlloc(transform.position, _attackRadius, _overlapColliders);

            for (var i = 0; i < overlapCount; i++)
            {
                if (_overlapColliders[i].TryGetComponent(out IDamageble iDamageble) && !(iDamageble is Player))
                {
                    if(IsRequiredTarget(iDamageble))
                        return iDamageble;
                }
            }

            return null;
        }

        protected abstract bool IsRequiredTarget(IDamageble iDamageble);
    }
}
