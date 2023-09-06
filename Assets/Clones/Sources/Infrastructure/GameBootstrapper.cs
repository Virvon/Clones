using Clones.UI;
using UnityEngine;

namespace Clones.Infrastructure
{
    public class GameBootstrapper : MonoBehaviour
    {
        [SerializeField] private LoadingPanel _loadingPanel;

        private Game _game;

        private void Awake()
        {
            _game = new Game(_loadingPanel);
            _game._stateMachine.Enter<BootstrapState>();

            DontDestroyOnLoad(this); 
        }
    }
}