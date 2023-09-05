namespace Clones.Infrastructure
{
    public class Game
    {
        public static IInputService InputService;

        public GameStateMachine _stateMachine { get; private set; }

        public Game()
        {
            _stateMachine = new GameStateMachine();
        }
    }
}