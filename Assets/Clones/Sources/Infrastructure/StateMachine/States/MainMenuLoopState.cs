using Agava.YandexGames;
using Clones.Services;
using Clones.StaticData;
using Clones.Types;
using Clones.UI;
using UnityEngine;

namespace Clones.Infrastructure
{
    public class MainMenuLoopState : IState
    {
        private readonly IMainMenuFactory _mainMenuFactory;
        private readonly IMainMenuStaticDataService _mainMenuStaticDataService;
        private readonly ICharacterFactory _characterFactory;

        public MainMenuLoopState(IMainMenuFactory mainMenuFactory, IMainMenuStaticDataService mainMenuStaticDataService, ICharacterFactory characterFactory)
        {
            _mainMenuFactory = mainMenuFactory;
            _mainMenuStaticDataService = mainMenuStaticDataService;
            _characterFactory = characterFactory;
        }

        public void Enter() => 
            CreateMainMenu();


        public void Exit() { }

        private void CreateMainMenu()
        {
            _mainMenuFactory.CreateMainMenu();
            _mainMenuFactory.CreatePlayButton();
            _mainMenuFactory.CreateStatsView();
            _mainMenuFactory.CreateUpgradeView();
            _mainMenuFactory.CreateScoreView();

            ClonesCardsView clonesCardsView = _mainMenuFactory.CreateClonesCardsView();
            WandsCardsView wandsCardsView = _mainMenuFactory.CreateWandsCardsView();

            _mainMenuFactory.CreateShowCardButtonds();
            CreateCards();

            _mainMenuFactory.CreateCloneModelPoint(_characterFactory);
            SelectCurrentOrDefaultCards(clonesCardsView, wandsCardsView);

            wandsCardsView.gameObject.SetActive(false);
        }

        private static void SelectCurrentOrDefaultCards(ClonesCardsView clonesCardsView, WandsCardsView wandsCardsView)
        {
            clonesCardsView.SelectCurrentOrDefault();
            wandsCardsView.SelectCurrentOrDefault();
        }

        private void CreateCards()
        {
            MainMenuStaticData menuData = _mainMenuStaticDataService.GetMainMenu();

            CreateClonesCards(menuData.CloneTypes);
            CreateWandsCards(menuData.WandTypes);
        }

        private void CreateClonesCards(CloneType[] types)
        {
            foreach (var type in types)
                _mainMenuFactory.CreateCloneCard(type);
        }

        private void CreateWandsCards(WandType[] types)
        {
            foreach (var type in types)
                _mainMenuFactory.CreateWandCard(type);
        }
    }
}