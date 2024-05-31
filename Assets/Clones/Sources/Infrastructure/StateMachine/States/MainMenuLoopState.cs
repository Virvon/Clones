using Clones.Services;
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
        private readonly IProgressReadersReporter _progressReadersReporter;

        public MainMenuLoopState(IMainMenuFactory mainMenuFactory, IMainMenuStaticDataService mainMenuStaticDataService, ICharacterFactory characterFactory, IProgressReadersReporter progressReadersReporter)
        {
            _mainMenuFactory = mainMenuFactory;
            _mainMenuStaticDataService = mainMenuStaticDataService;
            _characterFactory = characterFactory;
            _progressReadersReporter = progressReadersReporter;
        }

        public void Enter() => 
            CreateMainMenu();

        public void Exit() => 
            _progressReadersReporter.Clear();

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
            _progressReadersReporter.Register(_mainMenuFactory);
        }

        private void SelectCurrentOrDefaultCards(ClonesCardsView clonesCardsView, WandsCardsView wandsCardsView)
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