using UnityEngine;
using Clones.Services;
using Clones.UI;
using Clones.GameLogic;

namespace Clones.Infrastructure
{
    public class UiFactory : IUiFactory
    {
        private readonly IAssetProvider _assets;
        private readonly IPersistentProgressService _persistentProgressService;
        private readonly IGameStateMachine _stateMachine;

        public UiFactory(IAssetProvider assets, IPersistentProgressService persistentProgressService, IGameStateMachine stateMachine)
        {
            _assets = assets;
            _persistentProgressService = persistentProgressService;
            _stateMachine = stateMachine;
        }

        public GameObject CreateHud(IQuestsCreator questsCreator, GameObject playerObject, PlayerRevival playerRevival)
        {
            var hud = _assets.Instantiate(AssetPath.Hud);

            hud.GetComponentInChildren<Freezbar>()
                .Init(playerObject.GetComponentInChildren<PlayerFreezing>());

            hud.GetComponentInChildren<PlayerHealthbar>()
                .Init(playerObject.GetComponent<PlayerHealth>());

            hud.GetComponentInChildren<QuestPanel>()
                .Init(questsCreator, this);

            hud.GetComponentInChildren<MoneyView>()
                .Init(_persistentProgressService.Progress.Wallet);

            hud.GetComponentInChildren<DnaView>()
                .Init(_persistentProgressService.Progress.Wallet);

            hud.GetComponentInChildren<ChangeGameStateButton>()
                .Init(_stateMachine);

            hud.GetComponentInChildren<RevivalView>()
                .Init(playerRevival, hud.GetComponentInChildren<GameOverView>());

            return hud;
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