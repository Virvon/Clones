using System;

namespace Clones.Infrastructure
{
    public interface IGameStateMachine : IService
    {
        void Enter<TState, TPayload>(TPayload payload, Action callback) where TState : class, IPayloadState<TPayload>;
        void Enter<TState>() where TState : class, IState;
    }
}