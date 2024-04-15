﻿using Cinemachine;
using Clones.EducationLogic;
using Clones.GameLogic;
using Clones.Services;
using Clones.StateMachine;
using Clones.StaticData;
using Clones.UI;
using System.Collections.Generic;
using UnityEngine;

namespace Clones.Infrastructure
{
    public class EducationState : IState
    {
        private readonly IGameFacotry _gameFactory;
        private readonly IPartsFactory _partsFactory;
        private readonly IGameStaticDataService _gameStaticDataService;
        private readonly IPersistentProgressService _persistentProgress;
        private readonly IUiFactory _uiFactory;
        private readonly IInputService _inputService;
        private readonly IEducationFactory _educationFactory;
        private readonly ITimeScale _timeScale;
        private readonly ICoroutineRunner _coroutineRunner;
        private readonly ILocalization _localization;
        private readonly ICharacterFactory _characterFactory;

        private List<IDisable> _disables;
        private GameObject _playerObject;
        private IQuestsCreator _questCreator;
        private EducationEnemiesSpawner _enemiesSpawner;
        private FrameFocus _frameFocus;
        private GameObject _controlObject;
        private CinemachineVirtualCamera _educationVirtualCamera;

        public EducationState(IGameFacotry gameFactory, IPartsFactory partsFactory, IGameStaticDataService gameStaticDataService, IPersistentProgressService persistentProgress, IUiFactory uiFactory, IInputService inputService, IEducationFactory educationFactory, ITimeScale timeScale, ICoroutineRunner coroutineRunner, ILocalization localization, ICharacterFactory characterFactory)
        {
            _gameFactory = gameFactory;
            _partsFactory = partsFactory;
            _gameStaticDataService = gameStaticDataService;
            _persistentProgress = persistentProgress;
            _uiFactory = uiFactory;
            _inputService = inputService;
            _educationFactory = educationFactory;
            _coroutineRunner = coroutineRunner;
            _timeScale = timeScale;
            _localization = localization;

            _disables = new List<IDisable>();
            _characterFactory = characterFactory;
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

            _playerObject = _characterFactory.CreateCharacter(_partsFactory, itmesCounter);
            _characterFactory.CreateWand(_playerObject.GetComponent<WandBone>().Bone);

            _gameFactory.CreateVirtualCamera(_playerObject);
            _educationVirtualCamera = _educationFactory.CreateVirtualCamera();

            _uiFactory.CreateHud(_questCreator, _playerObject);
            _frameFocus = _uiFactory.CreateFrameFocus();
            _controlObject = _uiFactory.CreateControl(_playerObject.GetComponent<Player>());
            IOpenableView openableView = _uiFactory.CreateEducationOverView();

            EducationPreyResourcesSpawner spawner = _educationFactory.CreatePreyResourcesSpawner();

            CharacterAttack playerAttack = _playerObject.GetComponent<CharacterAttack>();
            QuestItemsDropper questItemsDropper = new(_partsFactory, playerAttack, _questCreator);
            CurrencyDropper currencyDropper = new(_partsFactory, playerAttack);

            _enemiesSpawner = _educationFactory.CreateEnemiesSpawner(_playerObject);

            PlayerDeath playerDeath = new(openableView, _playerObject.GetComponent<PlayerHealth>(), _timeScale, _enemiesSpawner);

            CreateEducationHandler().Handle();

            _questCreator.Create();
            spawner.Create();

            _disables.Add(currencyDropper);
            _disables.Add(questItemsDropper);
            _disables.Add(playerDeath);
        }

        private IQuestsCreator CreateEducationQuestCreator()
        {
            EducationQuestStaticData educationQuestStaticData = _gameStaticDataService.GetEducationQuest();
            IQuestsCreator questsCreator = new EducationQuestsCreator(educationQuestStaticData.GetAllQuests(_gameStaticDataService, _localization.GetIsoLanguage()), educationQuestStaticData.Reward, educationQuestStaticData.RewardIncrease, _persistentProgress);

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
            Waiter waiter = new(_coroutineRunner);
            ShowControlHandler showControlHandler = new(_inputService, _uiFactory.CreateDialogPanel(AssetPath.ShowControlDialogPanel));
            ShowFirstQuestHandler showFirstQuestHandler = new(_uiFactory.CreateDialogPanel(AssetPath.ShowFirstQuestDialogPanel), waiter, _frameFocus);
            ShowPreyResourcesHandler showPreyResourcesHandler = new(_playerObject.GetComponent<MiningState>(), _questCreator, _uiFactory.CreateDialogPanel(AssetPath.ShowPreyResourcesDialogPanel), _educationVirtualCamera, _controlObject, waiter);
            ShowSecondQuestHandler showSecondQuestHandler = new(_uiFactory.CreateDialogPanel(AssetPath.ShowSecondQuestDialogPanel), waiter);
            SpawnFirstWaveHandler spawnFirstWaveHandler = new(_enemiesSpawner, _uiFactory.CreateDialogPanel(AssetPath.SpawnFirstWaveDialogPanel), waiter);
            SpawnSecondWaveHandler spawnSecondWaveHandler = new(_enemiesSpawner, _questCreator);

            showControlHandler.Successor = showFirstQuestHandler;
            showFirstQuestHandler.Successor = showPreyResourcesHandler;
            showPreyResourcesHandler.Successor = showSecondQuestHandler;
            showSecondQuestHandler.Successor = spawnFirstWaveHandler;
            spawnFirstWaveHandler.Successor = spawnSecondWaveHandler;

            return showControlHandler;
        }
    }
}