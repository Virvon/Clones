using Clones.Audio;
using Clones.Data;
using Clones.Services;
using Clones.SFX;
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
        private readonly ISaveLoadService _saveLoadService;
        private readonly ILeaderboard _leaderboard;
        private readonly IProgressReadersReporter _progressReadersReporter;

        private MainMenuContainers _containers;
        private ClonesCardsView _clonesCardsView;
        private WandsCardsView _wandsCardsView;
        private HashSet<CloneType> _createdCloneTypes;
        private HashSet<WandType> _createdWandTypes;

        public MainMenuFactory(IAssetProvider assets, IGameStateMachine gameStateMachine, IPersistentProgressService persistentProgress, IMainMenuStaticDataService staticDataService, ISaveLoadService saveLoadService, ILeaderboard leaderboard, IProgressReadersReporter progressReadersReporter)
        {
            _assets = assets;
            _gameStateMachine = gameStateMachine;
            _persistentProgress = persistentProgress;
            _staticDataService = staticDataService;
            _saveLoadService = saveLoadService;
            _leaderboard = leaderboard;
            _progressReadersReporter = progressReadersReporter;

            _createdCloneTypes = new();
            _createdWandTypes = new();
        }

        public void UpdateProgress()
        {
            _clonesCardsView.Unsubscribe();
            _clonesCardsView.Clear();
            _wandsCardsView.Unsubscribe();
            _wandsCardsView.Clear();

            CreateClonesCards(_createdCloneTypes.ToArray());
            CreateWandsCards(_createdWandTypes.ToArray());

            _clonesCardsView.SelectCurrentOrDefault();
            _wandsCardsView.SelectCurrentOrDefault();
        }

        public GameObject CreateMainMenu()
        {
            MainMenuStaticData menuData = _staticDataService.GetMainMenu();

            GameObject menu = Object.Instantiate(menuData.Prefab);

            menu
                .GetComponentInChildren<MoneyView>()
                .Init(_persistentProgress);

            menu
                .GetComponentInChildren<DnaView>()
                .Init(_persistentProgress);

            foreach (var switcher in menu.GetComponents<AudioSwitcherSlider>())
                switcher.Init(_persistentProgress);

            _containers = menu.GetComponentInChildren<MainMenuContainers>();

            _containers
                .Settings
                .GetComponent<AudioSettingsSaver>()
                .Init(_saveLoadService);

            _containers
                .Leaderboard
                .GetComponent<LeaderboardView>()
                .Init(_leaderboard, this, _persistentProgress);

            _containers
                .AuthorizeView
                .GetComponent<AuthorizeView>()
                .Init(_persistentProgress, _saveLoadService, _progressReadersReporter);

            Register(menu);

            return menu;
        }

        public void CreateCloneCard(CloneType type)
        {
            CloneStaticData cloneStaticData = _staticDataService.GetClone(type);

            CloneCard card = Object.Instantiate(cloneStaticData.Card, _clonesCardsView.transform);

            _clonesCardsView.AddCard(card, type);

            bool isBuyed = _persistentProgress.Progress.AvailableClones.Clones.Any(cloneData => cloneData.Type == type);


            if (isBuyed == false)
            {
                card
                    .GetComponent<BuyCardView>()
                    .Init(cloneStaticData.BuyPrice, _persistentProgress.Progress.Wallet);
            }
            else
            {
                CloneData data = _persistentProgress.Progress.AvailableClones.Clones.Where(data => data.Type == type).First();

                card
                    .GetComponent<CloneLevelView>()
                    .Init(_persistentProgress, type);

                if (data.IsUsed)
                    card.GetComponent<DieClone>().Init(data.GetDisuseEndDate());
            }

            card.Init(isBuyed);
            _createdCloneTypes.Add(type);
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
            {
                cardObject
                    .GetComponent<BuyCardView>()
                    .Init(wandStaticData.BuyPrice, _persistentProgress.Progress.Wallet);
            }
            else
            {
                WandData data = _persistentProgress.Progress.AvailableWands.Wands.Where(data => data.Type == type).First();

                card
                    .GetComponent<WandLevelView>()
                    .Init(_persistentProgress, type);
            }

            _createdWandTypes.Add(type);
        }

        public ClonesCardsView CreateClonesCardsView()
        {
            GameObject viewObject = _assets.Instantiate(AssetPath.ClonesCardsView, _containers.ClonesCards.transform);

            _clonesCardsView = viewObject.GetComponentInChildren<ClonesCardsView>();
            _clonesCardsView.Init(_persistentProgress, _staticDataService, _saveLoadService);

            return _clonesCardsView;
        }

        public WandsCardsView CreateWandsCardsView()
        {
            GameObject viewObject = _assets.Instantiate(AssetPath.WandsCardsView, _containers.ClonesCards);

            _wandsCardsView = viewObject.GetComponentInChildren<WandsCardsView>();
            _wandsCardsView.Init(_persistentProgress, _staticDataService, _saveLoadService);

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
            
            ToggleWindows cloneCardsToggleWindows = clonesCardsShowButton.GetComponent<ToggleWindows>();
            ToggleWindows wandCardsToggleWindows = wandsCardsShowButton.GetComponent<ToggleWindows>();

            cloneCardsToggleWindows
                .Init(new List<GameObject> { wandsCardsShowButton, _clonesCardsView.gameObject }, new List<GameObject> { clonesCardsShowButton, _wandsCardsView.gameObject });

            wandCardsToggleWindows
                .Init(new List<GameObject> { clonesCardsShowButton, _wandsCardsView.gameObject }, new List<GameObject> { wandsCardsShowButton, _clonesCardsView.gameObject });

            _clonesCardsView
                .GetComponent<CardsScrollRect>()
                .Init(cloneCardsToggleWindows);

            _clonesCardsView
                .GetComponent<ButtonClickSound>()
                .Init(clonesCardsShowButton.GetComponentInChildren<Button>());
            
            _wandsCardsView
                .GetComponent<CardsScrollRect>()
                .Init(wandCardsToggleWindows);

            _wandsCardsView
                .GetComponent<ButtonClickSound>()
                .Init(wandsCardsShowButton.GetComponentInChildren<Button>());
        }

        public void CreateStatsView()
        {
            GameObject statsView = _assets.Instantiate(AssetPath.StatsView, _containers.StatsView);

            statsView.GetComponent<StatsView>()
                .Init(_persistentProgress);

            Register(statsView);
        }

        public void CreateUpgradeView()
        {
            GameObject upgradeView = _assets.Instantiate(AssetPath.UpgradeView, _containers.Buttons);

            upgradeView.GetComponent<UpgradeButtonsView>()
                .Init(_persistentProgress, _staticDataService, _saveLoadService);

            upgradeView.GetComponentInChildren<CloneUpgradeButton>()
                .Init(_persistentProgress);

            upgradeView.GetComponentInChildren<WandUpgradeButton>()
                .Init(_persistentProgress);

            Register(upgradeView);
        }

        public void CreateCloneModelPoint(ICharacterFactory characterFactory)
        {
            GameObject point = _assets.Instantiate(AssetPath.CloneModelPoint, _containers.Characters);

            point
                .GetComponent<WandModelCreator>()
                .Init(_wandsCardsView, characterFactory);


            point
                .GetComponent<CloneModelCreator>()
                .Init(_clonesCardsView, characterFactory);
        }

        public LeaderboardElement CreateLeaderboardElement(LeaderboardPlayer player, Transform parent)
        {
            LeaderboardElement leaderboardElement = _assets.Instantiate(AssetPath.LeaderboardElement, parent).GetComponent<LeaderboardElement>();

            leaderboardElement.Init(player.Rank, player.Name, player.Score);

            return leaderboardElement;

        }

        public void CreateScoreView()
        {
            GameObject scoreViewObject = _assets.Instantiate(AssetPath.ScoreView, _containers.Header);

            scoreViewObject
                .GetComponent<ScoreView>()
                .Init(_persistentProgress);

            Register(scoreViewObject);
        }

        private void Register(GameObject gameObject)
        {
            foreach (IProgressReader progressReader in gameObject.GetComponentsInChildren<IProgressReader>())
                _progressReadersReporter.Register(progressReader);
        }

        private void CreateClonesCards(CloneType[] types)
        {
            foreach (var type in types)
                CreateCloneCard(type);
        }

        private void CreateWandsCards(WandType[] types)
        {
            foreach (var type in types)
                CreateWandCard(type);
        }
    }
}