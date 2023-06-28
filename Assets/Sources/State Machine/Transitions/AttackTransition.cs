using UnityEngine;

namespace Clones.StateMachine
{
    public class AttackTransition : Transition
    {
        [SerializeField] private float _attackRadius;

        private readonly Collider[] _overlapColliders = new Collider[64];

        private void Update()
        {
            int overlapCount = Physics.OverlapSphereNonAlloc(transform.position, _attackRadius, _overlapColliders);

            for (var i = 0; i < overlapCount; i++)
            {
                if (_overlapColliders[i].TryGetComponent(out IDamageble idamageble) && _overlapColliders[i].TryGetComponent(out Enemy enemy))
                {
                    NeedTransit = true;
                }
            }
        }
    }
}
