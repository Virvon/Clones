using Clones.Services;
using Clones.UI;

namespace Clones.Infrastructure
{
    public class Game
    {
        public GameStateMachine StateMachine { get; private set; }

        public Game(LoadingPanel loadingPanel)
        {
            StateMachine = new GameStateMachine(new SceneLoader(), loadingPanel, AllServices.Instance);
        }
    }
}