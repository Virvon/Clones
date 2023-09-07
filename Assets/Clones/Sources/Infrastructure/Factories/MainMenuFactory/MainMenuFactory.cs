using System;
using Clones.UI;

namespace Clones.Infrastructure
{
    public class MainMenuFactory : IMainMenuFactory
    {
        public readonly IAssetProvider _assets;
        private readonly IGameStateMachine _gameStateMachine;

        public MainMenuFactory(IAssetProvider assets, IGameStateMachine gameStateMachine)
        {
            _assets = assets;
            _gameStateMachine = gameStateMachine;
        }

        public void CreateMainMenu()
        {
            var mainMenu = _assets.Instantiate(AssetPath.MainMenuPath);
            var playButton = mainMenu.GetComponentInChildren<PlayButton>();
            
            playButton.Init(_gameStateMachine);
        }
    }
}