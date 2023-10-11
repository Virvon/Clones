using System;

namespace Clones.Infrastructure
{
    public interface IPayloadState<TPayload> : IExitableState
    {
        void Enter(TPayload payload, Action callback);
    }
}