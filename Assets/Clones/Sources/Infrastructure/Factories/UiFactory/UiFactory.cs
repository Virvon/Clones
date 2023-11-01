using UnityEngine;
using Clones.Services;
using Clones.UI;
using Clones.GameLogic;
using Clones.Input;

namespace Clones.Infrastructure
{
    public class UiFactory : IUiFactory
    {
        private readonly IAssetProvider _assets;
        private readonly IPersistentProgressService _persistentProgressService;
        private readonly IGameStateMachine _stateMachine;
        private readonly IInputService _inputService;

        private GameObject _hud;

        public UiFactory(IAssetProvider assets, IPersistentProgressService persistentProgressService, IGameStateMachine stateMachine, IInputService inputService)
        {
            _assets = assets;
            _persistentProgressService = persistentProgressService;
            _stateMachine = stateMachine;
            _inputService = inputService;
        }

        public GameObject CreateHud(IQuestsCreator questsCreator, GameObject playerObject, PlayerRevival playerRevival)
        {
            _hud = _assets.Instantiate(AssetPath.Hud);

            _hud.GetComponentInChildren<Freezbar>()
                .Init(playerObject.GetComponentInChildren<PlayerFreezing>());

            _hud.GetComponentInChildren<PlayerHealthbar>()
                .Init(playerObject.GetComponent<PlayerHealth>());

            _hud.GetComponentInChildren<QuestPanel>()
                .Init(questsCreator, this);

            _hud.GetComponentInChildren<MoneyView>()
                .Init(_persistentProgressService.Progress.Wallet);

            _hud.GetComponentInChildren<DnaView>()
                .Init(_persistentProgressService.Progress.Wallet);

            _hud.GetComponentInChildren<ChangeGameStateButton>()
                .Init(_stateMachine);

            _hud.GetComponentInChildren<RevivalView>()
                .Init(playerRevival, _hud.GetComponentInChildren<GameOverView>());

            return _hud;
        }

        public void CreateControl(Player player)
        {
            GameObject control = _assets.Instantiate(_inputService.ControlPath, _hud.transform);

            if (control.TryGetComponent(out DesktopDirectionHandler desktopDirectionHandler))
                desktopDirectionHandler.Init(player);
        }

        public GameObject CreateQuestView(Quest quest, Transform parent)
        {
            var view = _assets.Instantiate(AssetPath.QuestView, parent);

            view.GetComponent<QuestView>()
                .Init(quest);

            return view;
        }
    }
}