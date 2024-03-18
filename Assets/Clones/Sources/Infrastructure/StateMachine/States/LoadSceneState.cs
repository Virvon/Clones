using Clones.UI;
using System;
using Clones.Services;

namespace Clones.Infrastructure
{
    public class LoadSceneState : IPayloadState<(string sceneName, bool showAdvertising)>
    {
        private readonly GameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly LoadingPanel _loadingPanel;
        private readonly IAdvertisingDisplay _advertising;

        public LoadSceneState(GameStateMachine gameStateMachine, SceneLoader sceneLoader, LoadingPanel loadingPanel, IAdvertisingDisplay advertising)
        {
            _stateMachine = gameStateMachine;
            _sceneLoader = sceneLoader;
            _loadingPanel = loadingPanel;
            _advertising = advertising;
        }

        public void Enter((string sceneName, bool showAdvertising) payload, Action callback)
        {
            if (payload.showAdvertising)
            {
                _sceneLoader.Load(payload.sceneName, callback: () =>
                {
                    _advertising.ShowInterstitialAd(callback);
                });
            }   
            else
            {
                _sceneLoader.Load(payload.sceneName, callback);
            }


            _loadingPanel.Open();
        }

        public void Exit() =>
            _loadingPanel.Close();
    }
}