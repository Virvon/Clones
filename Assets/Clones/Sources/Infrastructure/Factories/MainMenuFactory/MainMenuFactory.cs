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

            menu.GetComponentInChildren<PlayButton>()
                .Init(_gameStateMachine);

            menu.GetComponentInChildren<MoneyView>()
                .Init(_persistentProgress.Progress.Wallet);

            menu.GetComponentInChildren<DnaView>()
                .Init(_persistentProgress.Progress.Wallet);

            _containers = menu.GetComponentInChildren<MainMenuContainers>();

            return menu;
        }

        public void CreateCardClone(CloneType type)
        {
            CloneStaticData cardCloneData = _staticDataService.GetClone(type);

            CardsView cloneCardsView = _containers.ClonesCardsView;

            var cardObject = Object.Instantiate(cardCloneData.Card, cloneCardsView.transform);
            CloneCard card = cardObject.GetComponent<CloneCard>();

            cloneCardsView.AddCard(card);

            card.Init(cardCloneData.Helath, cardCloneData.IncreaseHealth, cardCloneData.Damage, cardCloneData.IncreaseDamage, cardCloneData.UpgradePrice, cardCloneData.IncreasePrice);
        }

        public void CreateWandCard(WandType type)
        {
            WandStaticData wandData = _staticDataService.GetWand(type);

            CardsView wandsCardsView = _containers.WandsCardsView;

            var cardObject = Object.Instantiate(wandData.Card, wandsCardsView.transform);
            CardWand card = cardObject.GetComponent<CardWand>();

            wandsCardsView.AddCard(card);

            card.Init(wandData.Damage, wandData.Cooldown);
        }
    }
}