using Clones.UI;
using System;

namespace Clones.Infrastructure
{
    public class LoadSceneState : IPayloadState<string>
    {
        private readonly GameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly LoadingPanel _loadingPanel;

        public LoadSceneState(GameStateMachine gameStateMachine, SceneLoader sceneLoader, LoadingPanel loadingPanel)
        {
            _stateMachine = gameStateMachine;
            _sceneLoader = sceneLoader;
            _loadingPanel = loadingPanel;
        }

        public void Enter(string sceneName, Action callback)
        {
            _sceneLoader.Load(sceneName, callback);

            _loadingPanel.Open();
        }

        public void Exit() =>
            _loadingPanel.Close();
    }
}