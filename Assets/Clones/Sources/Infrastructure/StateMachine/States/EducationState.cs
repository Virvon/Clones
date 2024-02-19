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

        public EducationState(IGameFacotry gameFactory, IPartsFactory partsFactory, IGameStaticDataService gameStaticDataService, IPersistentProgressService persistentProgress, IUiFactory uiFactory)
        {
            _gameFactory = gameFactory;
            _partsFactory = partsFactory;
            _gameStaticDataService = gameStaticDataService;
            _persistentProgress = persistentProgress;
            _uiFactory = uiFactory;
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
            IQuestsCreator questsCreator = CreateQuestCreator();
            IItemsCounter itmesCounter = CreateItemsCounter(questsCreator);
            GameObject playerObject = _gameFactory.CreatePlayer(_partsFactory, itmesCounter);

            _gameFactory.CreateVirtualCamera();

            _uiFactory.CreateHud(questsCreator, playerObject);
            _uiFactory.CreateControl(playerObject.GetComponent<Player>());
        }

        private IQuestsCreator CreateQuestCreator()
        {
            QuestStaticData questStaticData = _gameStaticDataService.GetQuest();
            IQuestsCreator questsCreator = new QuestsCreator(_persistentProgress, questStaticData.QuestItemTypes, ResourceMultiplier, questStaticData.ItemsCount, questStaticData.MinItemsCountPercentInQuest, questStaticData.Reward);

            return questsCreator;
        }

        private IItemsCounter CreateItemsCounter(IQuestsCreator questsCreator)
        {
            ItemsCounterStaticData itemsCounterStaticData = _gameStaticDataService.GetItemsCounter();
            int DNAReward = itemsCounterStaticData.DNAReward;
            int questItemReward = itemsCounterStaticData.CollectingItemReward;

            return new ItemsCounter(questsCreator, _persistentProgress, DNAReward, questItemReward);
        }

    }
}