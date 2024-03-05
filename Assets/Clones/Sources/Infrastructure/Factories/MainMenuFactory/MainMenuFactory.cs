using Clones.Data;
using Clones.Services;
using Clones.StaticData;
using Clones.Types;
using Clones.UI;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Clones.Infrastructure
{
    public class MainMenuFactory : IMainMenuFactory
    {
        private readonly IAssetProvider _assets;
        private readonly IGameStateMachine _gameStateMachine;
        private readonly IPersistentProgressService _persistentProgress;
        private readonly IMainMenuStaticDataService _staticDataService;

        private MainMenuContainers _containers;
        private ClonesCardsView _clonesCardsView;
        private WandsCardsView _wandsCardsView;

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

            GameObject menu = Object.Instantiate(menuData.Prefab);

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

            CloneCard card = Object.Instantiate(cloneStaticData.Card, _clonesCardsView.transform);

            _clonesCardsView.AddCard(card, type);

            bool isBuyed = _persistentProgress.Progress.AvailableClones.Clones.Any(cloneData => cloneData.Type == type);


            if (isBuyed == false)
                card.GetComponent<BuyCardView>().Init(cloneStaticData.BuyPrice, _persistentProgress.Progress.Wallet);
            else
            {
                CloneData data = _persistentProgress.Progress.AvailableClones.Clones.Where(data => data.Type == type).First();

                card.GetComponent<CloneLevelView>()
                    .Init(_persistentProgress, type);

                if (data.IsUsed)
                    card.GetComponent<DieClone>().Init(data.GetDisuseEndDate());
            }

            card.Init(isBuyed);
        }

        public void CreateWandCard(WandType type)
        {
            WandStaticData wandStaticData = _staticDataService.GetWand(type);

            var cardObject = Object.Instantiate(wandStaticData.Card, _wandsCardsView.transform);
            WandCard card = cardObject.GetComponent<WandCard>();

            _wandsCardsView.AddCard(card, type);

            bool isBuyed = _persistentProgress.Progress.AvailableWands.Wands.Any(wandData => wandData.Type == type);
            card.Init(isBuyed);

            if (isBuyed == false)
                cardObject.GetComponent<BuyCardView>().Init(wandStaticData.BuyPrice, _persistentProgress.Progress.Wallet);
        }

        public ClonesCardsView CreateClonesCardsView()
        {
            GameObject viewObject = _assets.Instantiate(AssetPath.ClonesCardsView, _containers.ClonesCards.transform);

            _clonesCardsView = viewObject.GetComponentInChildren<ClonesCardsView>();
            _clonesCardsView.Init(_persistentProgress, _staticDataService);

            return _clonesCardsView;
        }

        public WandsCardsView CreateWandsCardsView()
        {
            GameObject viewObject = _assets.Instantiate(AssetPath.WandsCardsView, _containers.ClonesCards);

            _wandsCardsView = viewObject.GetComponentInChildren<WandsCardsView>();
            _wandsCardsView.Init(_persistentProgress, _staticDataService);

            return _wandsCardsView;
        }

        public void CreatePlayButton()
        {
            GameObject button = _assets.Instantiate(AssetPath.PlayButton, _containers.Buttons);

            button.GetComponent<PlayButton>()
                .Init(_gameStateMachine);
        }

        public void CreateShowCardButtonds()
        {
            GameObject clonesCardsShowButton = _assets.Instantiate(AssetPath.ClonesCardsShowButton, _containers.Buttons);
            GameObject wandsCardsShowButton = _assets.Instantiate(AssetPath.WandsCardsShowButton, _containers.Buttons);

            Debug.Log(clonesCardsShowButton != null);
            Debug.Log(wandsCardsShowButton != null);

            GameObject clonesCardsShowButtonObject = clonesCardsShowButton.GetComponentInChildren<Button>().gameObject;
            GameObject wandsCardsShowButtonObject = wandsCardsShowButton.GetComponentInChildren<Button>().gameObject;

            clonesCardsShowButton.GetComponent<ToggleWindows>()
                .Init(new List<GameObject> { wandsCardsShowButtonObject, _clonesCardsView.gameObject }, new List<GameObject> { clonesCardsShowButtonObject, _wandsCardsView.gameObject });

            wandsCardsShowButton.GetComponent<ToggleWindows>()
                .Init(new List<GameObject> { clonesCardsShowButtonObject, _wandsCardsView.gameObject }, new List<GameObject> { wandsCardsShowButtonObject, _clonesCardsView.gameObject });

            clonesCardsShowButtonObject.SetActive(false);
        }

        public void CreateStatsView()
        {
            GameObject statsView = _assets.Instantiate(AssetPath.StatsView, _containers.StatsView);

            statsView.GetComponent<StatsView>()
                .Init(_persistentProgress);
        }

        public void CreateUpgradeView()
        {
            GameObject upgradeView = _assets.Instantiate(AssetPath.UpgradeView, _containers.Buttons);

            upgradeView.GetComponent<UpgradeButtonsView>()
                .Init(_persistentProgress, _staticDataService);

            upgradeView.GetComponentInChildren<CloneUpgradeButton>()
                .Init(_persistentProgress.Progress.Wallet);

            upgradeView.GetComponentInChildren<WandUpgradeButton>()
                .Init(_persistentProgress.Progress.Wallet);
        }
    }
}