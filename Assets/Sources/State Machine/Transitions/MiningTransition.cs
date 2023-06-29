using UnityEngine;

namespace Clones.StateMachine
{
    public class MiningTransition : Transition
    {
        [SerializeField] private float _miningRadius;

        private readonly Collider[] _overlapColliders = new Collider[64];

        private void Update()
        {
            int overlapCount = Physics.OverlapSphereNonAlloc(transform.position, _miningRadius, _overlapColliders);

            for (var i = 0; i < overlapCount; i++)
            {
                if (_overlapColliders[i].TryGetComponent(out Enemy enemy))
                {
                    return;
                }
            }

            for (var i = 0; i < overlapCount; i++)
            {
                if (_overlapColliders[i].TryGetComponent(out MiningFacility miningFacility))
                {
                    NeedTransit = true;
                }
            }
        }
    }
}
