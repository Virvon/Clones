using UnityEngine;

namespace Clones.Infrastructure
{
    public class GameBootstrapper : MonoBehaviour
    {
        private Game _gaeme;

        private void Awake()
        {
            _gaeme = new Game();
            _gaeme._stateMachine.Enter<BootstrapState>();

            DontDestroyOnLoad(this); 
        }
    }
}