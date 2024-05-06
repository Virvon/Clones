using UnityEngine;

namespace Clones.StateMachine
{
    public class IdleTransition : Transition
    {
        [SerializeField] private MiningState _miningState;

        private readonly Collider[] _overlapColliders = new Collider[64];

        protected override void Init() =>
            InputService.Deactivated += OnDeactivated;

        private void Update()
        {
            if (InputService.Direction != Vector2.zero)
                return;

            int overlapCount = Physics.OverlapSphereNonAlloc(transform.position, 10, _overlapColliders);

            for (var i = 0; i < overlapCount; i++)
            {
                if (_overlapColliders[i].TryGetComponent(out Enemy _) || (_overlapColliders[i].TryGetComponent(out PreyResource _) && _miningState.enabled))
                    return;
            }

            NeedTransit = true;
        }

        private void OnDisable() =>
            InputService.Deactivated -= OnDeactivated;

        private void OnDeactivated() => 
            NeedTransit = true;
    }
}