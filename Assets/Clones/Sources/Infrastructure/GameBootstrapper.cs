using Clones.UI;
using UnityEngine;

namespace Clones.Infrastructure
{
    public class GameBootstrapper : MonoBehaviour
    {
        [SerializeField] private LoadingPanel _loadingPanelPrefab;

        private Game _game;

        private void Awake()
        {
            _game = new Game(Instantiate(_loadingPanelPrefab));
            _game.StateMachine.Enter<BootstrapState>();

            DontDestroyOnLoad(this); 
        }
    }
}