namespace Clones.Infrastructure
{
    public interface IState
    {
        void Enter();

        void Exit();
    }
}