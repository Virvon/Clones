namespace Clones.StateMachine
{
    public class MovementTransition : Transition
    {
        protected override void Init() => 
            InputService.Activated += OnActivated;

        private void OnDisable() => 
            InputService.Activated -= OnActivated;

        private void OnActivated() => 
            NeedTransit = true;
    }
}