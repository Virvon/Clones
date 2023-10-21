using Clones.Services;
using Clones.StaticData;
using System;
using UnityEngine;

namespace Clones.Infrastructure
{
    public class MainMenuLoopState : IState
    {
        private readonly GameStateMachine _stateMachine;
        private readonly IMainMenuFactory _mainMenuFactory;
        private readonly IMainMenuStaticDataService _mainMenuStaticDataService;

        public MainMenuLoopState(GameStateMachine stateMachine, IMainMenuFactory mainMenuFactory, IMainMenuStaticDataService mainMenuStaticDataService)
        {
            _stateMachine = stateMachine;
            _mainMenuFactory = mainMenuFactory;
            _mainMenuStaticDataService = mainMenuStaticDataService;
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

            clonesCardsView.SelectCurrentOrDefault();
            wandsCardsView.SelectCurrentOrDefault();

            wandsCardsView.gameObject.SetActive(false);

            TestDateTime();
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

        private void TestDateTime()
        {
            DateTime dateTime = DateTime.Now;
            DateTime dateTime3;
            TimeSpan timeSpan = new TimeSpan();

            dateTime3 = new DateTime();
            dateTime3 = dateTime.AddMinutes(2);
            timeSpan = dateTime3 - dateTime;

            string time = dateTime.ToString();
            DateTime parce = DateTime.Parse(time);

            //Debug.Log("string " + time + " parce " + parce);
        }
    }
}