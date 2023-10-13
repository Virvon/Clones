using Clones.Services;
using Clones.StaticData;
using Clones.UI;
using UnityEngine;

namespace Clones.Infrastructure
{
    public class MainMenuFactory : IMainMenuFactory
    {
        private readonly IAssetProvider _assets;
        private readonly IGameStateMachine _gameStateMachine;
        private readonly IPersistentProgressService _persistentProgress;
        private readonly IMainMenuStaticDataService _staticDataService;

        private MainMenuContainers _containers;

        public MainMenuFactory(IAssetProvider assets, IGameStateMachine gameStateMachine, IPersistentProgressService persistentProgress, IMainMenuStaticDataService staticDataService)
        {
            _assets = assets;
            _gameStateMachine = gameStateMachine;
            _persistentProgress = persistentProgress;
            _staticDataService = staticDataService;
        }

        public GameObject CreateMainMenu()
        {
            MainMenuStaticData menuData = _staticDataService.GetMainMenu();

            var menu = Object.Instantiate(menuData.Prefab);

            menu.GetComponent<MainMenu>()
                .Init(menuData.CardCloneTypes);

            menu.GetComponentInChildren<PlayButton>()
                .Init(_gameStateMachine);

            menu.GetComponentInChildren<MoneyView>()
                .Init(_persistentProgress.Progress.Wallet);

            menu.GetComponentInChildren<DnaView>()
                .Init(_persistentProgress.Progress.Wallet);

            _containers = menu.GetComponentInChildren<MainMenuContainers>();

            return menu;
        }

        public void CreateCardClone(CardCloneType type)
        {
            CardCloneStaticData cardCloneData = _staticDataService.GetCardClone(type);

            var card = Object.Instantiate(cardCloneData.Prefab, _containers.CardClonesContainer.transform); ;

            card.GetComponent<CardClone>()
                .Init(cardCloneData.Helath, cardCloneData.IncreaseHealth, cardCloneData.Damage, cardCloneData.IncreaseDamage, cardCloneData.UpgradePrice, cardCloneData.IncreasePrice);
        }
    }
}