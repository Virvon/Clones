using Cinemachine;
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
        private readonly ITimeScaler _timeScale;
        private readonly ICoroutineRunner _coroutineRunner;
        private readonly ILocalization _localization;
        private readonly ICharacterFactory _characterFactory;
        private readonly ISaveLoadService _saveLoadService;

        private List<IDisabled> _disables;
        private GameObject _playerObject;
        private IQuestsCreator _questCreator;
        private EducationEnemiesSpawner _enemiesSpawner;
        private FrameFocus _frameFocus;
        private GameObject _controlObject;
        private CinemachineVirtualCamera _educationVirtualCamera;
        private IOpenableView _educationOverView;
        private QuestItemsDropper _questItemsDropper;
        private CurrencyDropper _currencyDropper;

        public EducationState(IGameFacotry gameFactory, IPartsFactory partsFactory, IGameStaticDataService gameStaticDataService, IPersistentProgressService persistentProgress, IUiFactory uiFactory, IInputService inputService, IEducationFactory educationFactory, ITimeScaler timeScale, ICoroutineRunner coroutineRunner, ILocalization localization, ICharacterFactory characterFactory, ISaveLoadService saveLoadService)
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
            _saveLoadService = saveLoadService;

            _disables = new List<IDisabled>();
            _characterFactory = characterFactory;
        }

        public void Enter() =>
            CreateEducation();

        public void Exit()
        {
            _saveLoadService.SaveProgress();

            foreach (var disable in _disables)
                disable.Disable();
        }

        private void CreateEducation()
        {
            _questCreator = CreateEducationQuestCreator();
            IItemsCounter itmesCounter = CreateItemsCounter(_questCreator);

            CreateCharacter(itmesCounter);
            CreateCameras();
            CreateHud();

            EducationPreyResourcesSpawner spawner = _educationFactory.CreatePreyResourcesSpawner();

            CreateDroppers();

            _enemiesSpawner = _educationFactory.CreateEnemiesSpawner(_playerObject);

            PlayerDeath playerDeath = new(_educationOverView, _playerObject.GetComponent<PlayerHealth>(), _timeScale, _enemiesSpawner);

            AddDisables(playerDeath);

            CreateEducationHandler().Handle();

            _questCreator.Create();
            spawner.Create();
        }

        private void AddDisables(PlayerDeath playerDeath)
        {
            _disables.Add(_currencyDropper);
            _disables.Add(_questItemsDropper);
            _disables.Add(playerDeath);
        }

        private void CreateDroppers()
        {
            IKiller playerAttack = _playerObject.GetComponent<IKiller>();
            _questItemsDropper = new(_partsFactory, playerAttack, _questCreator);
            _currencyDropper = new(_partsFactory, playerAttack);
        }

        private void CreateHud()
        {
            _uiFactory.CreateHud(_questCreator, _playerObject);
            _uiFactory.CreateWallet();
            _frameFocus = _uiFactory.CreateFrameFocus();
            _controlObject = _uiFactory.CreateControl(_playerObject.GetComponent<Player>());
            _educationOverView = _uiFactory.CreateEducationOverView();
            _uiFactory.CreateAudioButton();
        }

        private void CreateCameras()
        {
            _gameFactory.CreateVirtualCamera(_playerObject);
            _educationVirtualCamera = _educationFactory.CreateVirtualCamera();
        }

        private void CreateCharacter(IItemsCounter itmesCounter)
        {
            _playerObject = _characterFactory.CreateCharacter(_partsFactory, itmesCounter);
            _characterFactory.CreateWand(_playerObject.GetComponent<WandBone>().Bone);
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
            DirectionMarker directionMarker = _educationFactory.CreateDirectionMarker(_playerObject.transform);

            Waiter waiter = new(_coroutineRunner);
            ShowControlHandler showControlHandler = new(_inputService, _uiFactory.CreateDialogPanel(AssetPath.ShowControlDialogPanel));
            ShowFirstQuestHandler showFirstQuestHandler = new(_uiFactory.CreateDialogPanel(AssetPath.ShowFirstQuestDialogPanel), waiter, _frameFocus);
            ShowPreyResourcesHandler showPreyResourcesHandler = new(_playerObject.GetComponent<MiningState>(), _questCreator, _uiFactory.CreateDialogPanel(AssetPath.ShowPreyResourcesDialogPanel), _educationVirtualCamera, _controlObject, waiter, directionMarker, _educationFactory.CreateFirstDirectionMarkerTarget());
            ShowSecondQuestHandler showSecondQuestHandler = new(_uiFactory.CreateDialogPanel(AssetPath.ShowSecondQuestDialogPanel), waiter, directionMarker, _educationFactory.CreateSecondDirectionMarkerTarget());
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