namespace Clones.StateMachine
{
    public class MovementTransition : Transition
    {
        protected override void OnEnable()
        {
            DirectionHandler.Activated += OnActivated;
            base.OnEnable();
        }

        private void OnDisable() => DirectionHandler.Activated -= OnActivated;

        private void OnActivated() => NeedTransit = true;
    }
}
