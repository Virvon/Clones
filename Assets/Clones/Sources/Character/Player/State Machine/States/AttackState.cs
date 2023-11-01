using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Clones.StateMachine
{
    public abstract class AttackState : State
    {
        [SerializeField] private CharacterAttack _characterAttack;

        private readonly Collider[] _overlapColliders = new Collider[128];

        private float _attackRadius;
        private float _lookRotationSpeed;
        private IDamageable _target;

        public event Action<GameObject> TargetSelected;
        public event Action TargetRejected;

        private void Update() => 
            Attack();

        private void OnEnable()
        {
            if (_target != null)
                OnTargetDied(null);
        }

        private void OnDisable() => 
            TargetRejected?.Invoke();

        public void Init(float attackRadius, float lookRotaionSpeed)
        {
            _attackRadius = attackRadius;
            _lookRotationSpeed = lookRotaionSpeed;
        }

        private void Attack()
        {
            if (_target == null)
            {
                if (TryGetNearTarget(out _target))
                {
                    _target.Died += OnTargetDied;
                    TargetSelected?.Invoke(((MonoBehaviour)_target).gameObject);
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
            List<IDamageable> damageables = new();

            int overlapCount = Physics.OverlapSphereNonAlloc(transform.position, _attackRadius, _overlapColliders);

            for (var i = 0; i < overlapCount; i++)
            {
                if (_overlapColliders[i].TryGetComponent(out IDamageable damageble) && damageble is not Player)
                {
                    if (IsRequiredTarget(damageble))
                        damageables.Add(damageble);
                }
            }

            if(damageables.Count == 0)
            {
                target = null;
                return false;
            }
            else
            {
                var orderDamageables = damageables.OrderBy(damageble => Vector3.Distance(transform.position, ((MonoBehaviour)damageble).transform.position));

                target = orderDamageables.First();
                return true;
            }
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
