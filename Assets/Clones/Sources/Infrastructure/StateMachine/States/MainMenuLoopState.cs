﻿using Clones.Services;
using Clones.StaticData;
using Clones.Types;
using Clones.UI;

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

        public void Enter()
        {
            _mainMenuFactory.CreateMainMenu();
            _mainMenuFactory.CreatePlayButton();
            _mainMenuFactory.CreateStatsView();
            _mainMenuFactory.CreateUpgradeView();

            ClonesCardsView clonesCardsView = _mainMenuFactory.CreateClonesCardsView();
            WandsCardsView wandsCardsView = _mainMenuFactory.CreateWandsCardsView();

            _mainMenuFactory.CreateShowCardButtonds();

            MainMenuStaticData menuData = _mainMenuStaticDataService.GetMainMenu();

            CreateClonesCards(menuData.CloneTypes);
            CreateWandsCards(menuData.WandTypes);

            _mainMenuFactory.CreateCloneModelPoint(_characterFactory);

            clonesCardsView.SelectCurrentOrDefault();
            wandsCardsView.SelectCurrentOrDefault();

            wandsCardsView.gameObject.SetActive(false);
        } 

        public void Exit()
        {
            
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