using Clones.EducationLogic;
using Clones.GameLogic;
using Clones.Services;
using Clones.StateMachine;
using Clones.StaticData;
using System.Collections.Generic;
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

        private List<IDisable> _disables;
        private GameObject _playerObject;
        private IQuestsCreator _questCreator;
        private EducationEnemiesSpawner _enemiesSpawner;

        public EducationState(IGameFacotry gameFactory, IPartsFactory partsFactory, IGameStaticDataService gameStaticDataService, IPersistentProgressService persistentProgress, IUiFactory uiFactory, IInputService inputService, IEducationFactory educationFactory)
        {
            _gameFactory = gameFactory;
            _partsFactory = partsFactory;
            _gameStaticDataService = gameStaticDataService;
            _persistentProgress = persistentProgress;
            _uiFactory = uiFactory;
            _inputService = inputService;
            _educationFactory = educationFactory;

            _disables = new List<IDisable>();
        }

        public void Enter()
        {
            CreateEducation();
        }

        public void Exit()
        {
            foreach (var disable in _disables)
                disable.OnDisable();
        }

        private void CreateEducation()
        {
            _questCreator = CreateEducationQuestCreator();
            IItemsCounter itmesCounter = CreateItemsCounter(_questCreator);
            _playerObject = _gameFactory.CreatePlayer(_partsFactory, itmesCounter);

            _gameFactory.CreateVirtualCamera();

            _uiFactory.CreateHud(_questCreator, _playerObject);
            _uiFactory.CreateControl(_playerObject.GetComponent<Player>());

            EducationPreyResourcesSpawner spawner = _educationFactory.CreatePreyResourcesSpawner();

            CharacterAttack playerAttack = _playerObject.GetComponent<CharacterAttack>();
            QuestItemsDropper questItemsDropper = new(_partsFactory, playerAttack, _questCreator);
            CurrencyDropper currencyDropper = new(_partsFactory, playerAttack);

            _enemiesSpawner = _educationFactory.CreateEnemiesSpawner(_playerObject);

            CreateEducationHandler().Handle();

            _questCreator.Create();
            spawner.Create();

            _disables.Add(currencyDropper);
            _disables.Add(questItemsDropper);
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
            ShowFirstQuestHandler showFirstQuestHandler = new();
            ShowPreyResourcesHandler showPreyResourcesHandler = new(_playerObject.GetComponent<MiningState>(), _questCreator);
            ShowSecondQuestHandler showSecondQuestHandler = new();
            SpawnWaveHandler spawnWaveHandler = new(_enemiesSpawner);

            showControlHandler.Successor = showFirstQuestHandler;
            showFirstQuestHandler.Successor = showPreyResourcesHandler;
            showPreyResourcesHandler.Successor = showSecondQuestHandler;
            showSecondQuestHandler.Successor = spawnWaveHandler;

            return showControlHandler;
        }
    }
}