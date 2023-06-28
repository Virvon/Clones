namespace Clones.StateMachine
{
    public class IdleTransition : Transition
    {
        protected override void OnEnable()
        {
            DirectionHandler.Deactivated += OnDeactivated;
            base.OnEnable();
        }

        private void OnDisable() => DirectionHandler.Deactivated -= OnDeactivated;

        private void OnDeactivated() => NeedTransit = true;
    }
}
