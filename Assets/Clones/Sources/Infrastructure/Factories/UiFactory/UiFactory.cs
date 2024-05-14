using UnityEngine;
using Clones.Services;
using Clones.UI;
using Clones.GameLogic;
using Clones.Input;
using Clones.SFX;
using Clones.Audio;

namespace Clones.Infrastructure
{
    public class UiFactory : IUiFactory
    {
        private readonly IAssetProvider _assets;
        private readonly IPersistentProgressService _persistentProgressService;
        private readonly IGameStateMachine _stateMachine;
        private readonly IInputService _inputService;
        private readonly ITimeScaler _timeScaler;

        private GameObject _hud;
        private GameOverView _gameOverView;
        private QuestPanel _questPanel;
        private GameObject _control;
        private GameRevivalView _gameRevivalView;
        private GameObject _wallet;

        public UiFactory(IAssetProvider assets, IPersistentProgressService persistentProgressService, IGameStateMachine stateMachine, IInputService inputService, ITimeScaler timeScaler)
        {
            _assets = assets;
            _persistentProgressService = persistentProgressService;
            _stateMachine = stateMachine;
            _inputService = inputService;
            _timeScaler = timeScaler;
        }

        public GameObject CreateHud(IQuestsCreator questsCreator, GameObject playerObject)
        {
            _hud = _assets.Instantiate(AssetPath.Hud);

            playerObject
                .GetComponentInChildren<FreezbarReporter>()
                .Init(_hud.GetComponentInChildren<Freezbar>());

            _hud
                .GetComponentInChildren<PlayerHealthbar>()
                .Init(playerObject.GetComponent<PlayerHealth>());

            _questPanel = _hud.GetComponentInChildren<QuestPanel>();
            _questPanel.Init(questsCreator, this);

            _hud
                .GetComponentInChildren<QuestSound>()
                .Init(questsCreator);

            return _hud;
        }

        public void CreateWallet()
        {
            _wallet = _assets.Instantiate(AssetPath.Wallet, _hud.transform);
            
            _wallet
                .GetComponentInChildren<DnaView>()
                .Init(_persistentProgressService.Progress.Wallet);

            _wallet
                .GetComponentInChildren<MoneyView>()
                .Init(_persistentProgressService.Progress.Wallet);
        }

        public GameObject CreateControl(Player player)
        {
            _control = _assets.Instantiate(_inputService.ControlPath, _hud.transform);

            if (_control.TryGetComponent(out DesktopDirectionHandler desktopDirectionHandler))
                desktopDirectionHandler.Init(player);

            return _control;
        }

        public GameObject CreateQuestView(Quest quest, Transform parent)
        {
            var view = _assets.Instantiate(AssetPath.QuestView, parent);

            view.GetComponent<QuestView>()
                .Init(quest);

            return view;
        }

        public void CreateGameRevivleView(IPlayerRevival playerRevival)
        {
            GameObject gameRevivleView = _assets.Instantiate(AssetPath.GameRevivleView, _hud.transform);

            _gameRevivalView = gameRevivleView.GetComponent<GameRevivalView>();

            _gameRevivalView.Init(playerRevival, _gameOverView);

            _gameRevivalView
                .GetComponent<RevivalButton>()
                .Init(playerRevival, _gameOverView);
        }

        public void CreateGameOverView()
        {
            GameObject gameOverView = _assets.Instantiate(AssetPath.GameOverView, _hud.transform);

            _gameOverView = gameOverView.GetComponent<GameOverView>();
            _gameOverView.Close();

            _hud.GetComponentInChildren<ChangeGameStateButton>()
                .Init(_stateMachine);
        }

        public IOpenableView CreateEducationOverView()
        {
            GameObject educationOverViewObject = _assets.Instantiate(AssetPath.EducationOverView, _hud.transform);

            GameOverView educationOverView = educationOverViewObject.GetComponent<GameOverView>();

            _hud.GetComponentInChildren<ChangeGameStateButton>()
                .Init(_stateMachine);

            return educationOverView;
        }

        public DialogPanel CreateDialogPanel(string path)
        {
            GameObject dialogPanelObject = _assets.Instantiate(path, _hud.transform);
            DialogPanel dialogPanel = dialogPanelObject.GetComponent<DialogPanel>();

            dialogPanel.Disable();
            
            return dialogPanel;
        }

        public FrameFocus CreateFrameFocus()
        {
            GameObject frameFocusObject = _assets.Instantiate(AssetPath.FrameFocus, _questPanel.transform);
            FrameFocus frameFocus = frameFocusObject.GetComponent<FrameFocus>();

            frameFocus.Init();

            return frameFocus;
        }

        public void CreateGameSettings(IDestoryableEnemies destoryableEnemies, PlayerHealth playerHealth)
        {
            GameObject settings = _assets.Instantiate(AssetPath.GameSettings, _hud.transform);

            settings
                .GetComponent<AudioSwitcherButton>()
                .Init(_persistentProgressService);

            settings
                .GetComponent<ExitToMenuButton>()
                .Init(_control, destoryableEnemies, playerHealth, _gameOverView, _timeScaler, _gameRevivalView);
        }

        public void CreateAudioButton()
        {
            _assets
                .Instantiate(AssetPath.AudioButton, _hud.transform)
                .GetComponent<AudioSwitcherButton>()
                .Init(_persistentProgressService);
        }

        public void CreateScoreCounterPerGame(IMainScoreCounter mainScoreCounter)
        {
            _assets
                .Instantiate(AssetPath.ScoreCounterPerGame, _wallet.transform)
                .GetComponent<ScoreCountPerGame>()
                .Init(mainScoreCounter);
        }
    }
}