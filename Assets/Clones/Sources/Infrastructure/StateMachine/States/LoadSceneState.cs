using Clones.UI;
using UnityEngine;

namespace Clones.Infrastructure
{
    public class LoadSceneState : IPayloadState<string>
    {
        private readonly GameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly LoadingPanel _loadingPanel;
        private readonly IGameFactory _gameFactory;

        public LoadSceneState(GameStateMachine gameStateMachine, SceneLoader sceneLoader, LoadingPanel loadingPanel, IGameFactory gameFactory)
        {
            _stateMachine = gameStateMachine;
            _sceneLoader = sceneLoader;
            _loadingPanel = loadingPanel;
            _gameFactory = gameFactory;
        }

        public void Enter(string sceneName)
        {
            _sceneLoader.Load(sceneName, callback: OnLoaded);
            _loadingPanel.Open();
        }

        public void Exit() => 
            _loadingPanel.Close();

        private void OnLoaded()
        {
            GameObject player = _gameFactory.CreatePlayer();

            _stateMachine.Enter<GameLoopState>();
        }
    }
}