using UnityEngine;

namespace Clones.StateMachine
{
    public abstract class AttackTransition : Transition
    {
        [SerializeField] private float _attackRadius;

        protected readonly Collider[] _overlapColliders = new Collider[64];

        private void Update()
        {
            if(IsTargetsEnter(Physics.OverlapSphereNonAlloc(transform.position, _attackRadius, _overlapColliders)))
                NeedTransit = true;
        }

        protected virtual bool IsTargetsEnter(int overlapCount)
        {
            for (var i = 0; i < overlapCount; i++)
            {
                if (_overlapColliders[i].TryGetComponent(out IDamageable iDamageable))
                {
                    if(IsRequiredTarget(iDamageable))
                        return true;
                }
            }

            return false;
        }

        protected abstract bool IsRequiredTarget(IDamageable iDamageble);
    }
}
