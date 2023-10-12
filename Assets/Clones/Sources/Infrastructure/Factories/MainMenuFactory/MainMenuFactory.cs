﻿using Clones.Services;
using Clones.UI;

namespace Clones.Infrastructure
{
    public class MainMenuFactory : IMainMenuFactory
    {
        private readonly IAssetProvider _assets;
        private readonly IGameStateMachine _gameStateMachine;
        private readonly IPersistentProgressService _persistentProgress;

        public MainMenuFactory(IAssetProvider assets, IGameStateMachine gameStateMachine, IPersistentProgressService persistentProgress)
        {
            _assets = assets;
            _gameStateMachine = gameStateMachine;
            _persistentProgress = persistentProgress;
        }

        public void CreateMainMenu()
        {
            var mainMenu = _assets.Instantiate(AssetPath.MainMenu);

            mainMenu.GetComponentInChildren<PlayButton>()
                .Init(_gameStateMachine);

            mainMenu.GetComponentInChildren<MoneyView>()
                .Init(_persistentProgress.Progress.Wallet);

            mainMenu.GetComponentInChildren<DnaView>()
                .Init(_persistentProgress.Progress.Wallet);
        }
    }
}