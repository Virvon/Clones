using System;
using UnityEngine;

namespace Clones.StateMachine
{
    public abstract class AttackState : State
    {
        [SerializeField] private float _attackRadius;
        [SerializeField] private float _lookRotationSpeed;
        [SerializeField] private CharacterAttack _characterAttack;

        private readonly Collider[] _overlapColliders = new Collider[64];

        private IDamageable _target;

        public event Action<Transform> TargetSelected;
        public event Action TargetRejected;

        private void Update() => 
            Attack();

        private void OnEnable()
        {
            if (_target != null)
                OnTargetDied(null);
        }

        private void OnDisable() => TargetRejected?.Invoke();

        private void Attack()
        {
            if (_target == null)
            {
                if (TryGetNearTarget(out _target))
                {
                    _target.Died += OnTargetDied;
                    TargetSelected?.Invoke(((MonoBehaviour)_target).transform);
                }
                else
                    return;
            }

            if(_target.IsAlive)
            {
                _characterAttack.TryAttack(_target);
                RotateTo(((MonoBehaviour)_target).transform.position);
            }
        }

        private void RotateTo(Vector3 targetPosition)
        {
            targetPosition.y = transform.position.y;

            var direction = Quaternion.LookRotation(targetPosition - transform.position, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, direction, _lookRotationSpeed * Time.deltaTime);
        }

        private bool TryGetNearTarget(out IDamageable target)
        {
            int overlapCount = Physics.OverlapSphereNonAlloc(transform.position, _attackRadius, _overlapColliders);

            for (var i = 0; i < overlapCount; i++)
            {
                if (_overlapColliders[i].TryGetComponent(out IDamageable iDamageble) && !(iDamageble is Player))
                {
                    if (IsRequiredTarget(iDamageble))
                    {
                        target = iDamageble;
                        return true;
                    }
                }
            }

            target = null;
            return false;
        }

        private void OnTargetDied(IDamageable damageble)
        {
            _target.Died -= OnTargetDied;
            TargetRejected?.Invoke();
            _target = null;
        }

        protected abstract bool IsRequiredTarget(IDamageable iDamageble);
    }
}
