using UnityEngine;

namespace Clones.StateMachine
{
    public class MovementTransition : Transition
    {
        private void OnDisable() => 
            InputService.Activated -= OnActivated;

        protected override void Init()
        {
            InputService.Activated += OnActivated;
        }

        private void OnActivated()
        {
            NeedTransit = true;
        }
    }
}
