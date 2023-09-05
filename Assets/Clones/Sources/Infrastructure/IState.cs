namespace Clones.Infrastructure
{
    public interface IState : IExitableState
    {
        void Enter();
    }
}