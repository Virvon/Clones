using UnityEngine;

namespace Clones.StateMachine
{
    public class IdleTransition : Transition
    {
        private readonly Collider[] _overlapColliders = new Collider[64];

        private void OnDisable() => InputService.Deactivated -= OnDeactivated;

        private void Update()
        {
            if (InputService.Direction != Vector2.zero)
                return;

            int overlapCount = Physics.OverlapSphereNonAlloc(transform.position, 10, _overlapColliders);

            for (var i = 0; i < overlapCount; i++)
            {
                if ((_overlapColliders[i].TryGetComponent(out Enemy enemy)) || (_overlapColliders[i].TryGetComponent(out PreyResource miningFacility)))
                    return;
            }

            NeedTransit = true;
        }

        protected override void Init() => 
            InputService.Deactivated += OnDeactivated;

        private void OnDeactivated() => 
            NeedTransit = true;
    }
}
