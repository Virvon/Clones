using Clones.EducationLogic;
using Clones.GameLogic;
using Clones.Services;
using Clones.StaticData;
using UnityEngine;

namespace Clones.Infrastructure
{
    public class EducationState : IState
    {
        private const int ResourceMultiplier = 1;

        private readonly IGameFacotry _gameFactory;
        private readonly IPartsFactory _partsFactory;
        private readonly IGameStaticDataService _gameStaticDataService;
        private readonly IPersistentProgressService _persistentProgress;
        private readonly IUiFactory _uiFactory;
        private readonly IInputService _inputService;
        private readonly IEducationFactory _educationFactory;

        public EducationState(IGameFacotry gameFactory, IPartsFactory partsFactory, IGameStaticDataService gameStaticDataService, IPersistentProgressService persistentProgress, IUiFactory uiFactory, IInputService inputService, IEducationFactory educationFactory)
        {
            _gameFactory = gameFactory;
            _partsFactory = partsFactory;
            _gameStaticDataService = gameStaticDataService;
            _persistentProgress = persistentProgress;
            _uiFactory = uiFactory;
            _inputService = inputService;
            _educationFactory = educationFactory;
        }

        public void Enter()
        {
            CreateEducation();
        }

        public void Exit()
        {
            
        }

        private void CreateEducation()
        {
            IQuestsCreator questsCreator = CreateEducationQuestCreator();
            IItemsCounter itmesCounter = CreateItemsCounter(questsCreator);
            GameObject playerObject = _gameFactory.CreatePlayer(_partsFactory, itmesCounter);

            _gameFactory.CreateVirtualCamera();

            _uiFactory.CreateHud(questsCreator, playerObject);
            _uiFactory.CreateControl(playerObject.GetComponent<Player>());

            CreateEducationHandler().Handle();

            EducationPreyResourcesSpawner spawner = _educationFactory.CreateSpawner();

            questsCreator.Create();
            spawner.Create();
        }

        private IQuestsCreator CreateEducationQuestCreator()
        {
            EducationQuestStaticData educationQuestStaticData = _gameStaticDataService.GetEducationQuest();
            IQuestsCreator questsCreator = new EducationQuestsCreator(educationQuestStaticData.GetAllQuests(), educationQuestStaticData.Reward, educationQuestStaticData.RewardIncrease, _persistentProgress);

            return questsCreator;
        }

        private IItemsCounter CreateItemsCounter(IQuestsCreator questsCreator)
        {
            ItemsCounterStaticData itemsCounterStaticData = _gameStaticDataService.GetItemsCounter();
            int DNAReward = itemsCounterStaticData.DNAReward;
            int questItemReward = itemsCounterStaticData.CollectingItemReward;

            return new ItemsCounter(questsCreator, _persistentProgress, DNAReward, questItemReward);
        }

        private EducationHandler CreateEducationHandler()
        {
            ShowControlHandler showControlHandler = new(_inputService);
            ShowQuestHandler showQuestHandler = new();
            ShowPreyResourcesHandler showPreyResourcesHandler = new();

            showControlHandler.Successor = showQuestHandler;
            showQuestHandler.Successor = showPreyResourcesHandler;

            return showControlHandler;
        }
    }
}