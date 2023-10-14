using Clones.Data;
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

        public void CreateCloneCard(CloneType type)
        {
            CloneStaticData cloneStaticData = _staticDataService.GetClone(type);

            CardsView cloneCardsView = _containers.ClonesCardsView;

            var cardObject = Object.Instantiate(cloneStaticData.Card, cloneCardsView.transform);
            CloneCard card = cardObject.GetComponent<CloneCard>();

            cloneCardsView.AddCard(card);

            cardObject.GetComponent<BuyCardView>()
                .Init(cloneStaticData.BuyPrice, _persistentProgress.Progress.Wallet);

            if (_persistentProgress.Progress.AvailableClones.Clones.TryGetValue(type, out CloneData data))
                card.Init(data.Health, cloneStaticData.IncreaseHealth, data.Damage, cloneStaticData.IncreaseDamage, cloneStaticData.UpgradePrice, cloneStaticData.IncreasePrice);

            card.Init(cloneStaticData.Helath, cloneStaticData.IncreaseHealth, cloneStaticData.Damage, cloneStaticData.IncreaseDamage, cloneStaticData.UpgradePrice, cloneStaticData.IncreasePrice);
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