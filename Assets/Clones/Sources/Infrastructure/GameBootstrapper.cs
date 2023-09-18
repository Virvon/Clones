using Clones.UI;
using UnityEngine;

namespace Clones.Infrastructure
{
    public class GameBootstrapper : MonoBehaviour, ICoroutineRunner
    {
        [SerializeField] private LoadingPanel _loadingPanelPrefab;

        private Game _game;

        private void Awake()
        {
            _game = new Game(Instantiate(_loadingPanelPrefab), this);
            _game._stateMachine.Enter<BootstrapState>();

            DontDestroyOnLoad(this); 
        }
    }
}