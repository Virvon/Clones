using Clones.UI;
using System;
using Clones.Services;

namespace Clones.Infrastructure
{
    public class LoadSceneState : IPayloadState<(string sceneName, bool showAdvertising)>
    {
        private readonly SceneLoader _sceneLoader;
        private readonly LoadingPanel _loadingPanel;
        private readonly IAdvertisingDisplay _advertising;

        public LoadSceneState(SceneLoader sceneLoader, LoadingPanel loadingPanel, IAdvertisingDisplay advertising)
        {
            _sceneLoader = sceneLoader;
            _loadingPanel = loadingPanel;
            _advertising = advertising;
        }

        public void Enter((string sceneName, bool showAdvertising) payload, Action callback)
        {
            if (payload.showAdvertising)
            {
                _sceneLoader.Load(payload.sceneName, false, callback);
                _advertising.ShowInterstitialAd(callback: () => _sceneLoader.AllowSceneActivation());
            }   
            else
            {
                _sceneLoader.Load(payload.sceneName, callback: callback);
            }

            _loadingPanel.Open();
        }

        public void Exit() =>
            _loadingPanel.Close();
    }
}