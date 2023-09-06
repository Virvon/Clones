namespace Clones.Infrastructure
{
    public interface IGameStateMachine
    {
        void Enter<TState, TPayload>(TPayload payload) where TState : class, IPayloadState<TPayload>;
        void Enter<TState>() where TState : class, IState;
    }
}