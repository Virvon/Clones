using Clones.GameLogic;
using Clones.StateMachine;
using Clones.UI;
using System;
using UnityEngine;

namespace Clones.EducationLogic
{
    public class ShowPreyResourcesHandler : EducationHandler
    {
        private readonly MiningState _miningState;
        private readonly IQuestsCreator _questsCreator;
        private readonly DialogPanel _dialogPanel;

        public ShowPreyResourcesHandler(MiningState miningState, IQuestsCreator questsCreator, DialogPanel dialogPanel)
        {
            _miningState = miningState;
            _questsCreator = questsCreator;
            _dialogPanel = dialogPanel;
        }

        public override void Handle()
        {
            _dialogPanel.Open();
            Debug.Log("Сломай эти штуки. Для этого остановись возле них");

            _miningState.TargetSelected += OnTargetSelected;
        }

        private void OnTargetSelected(GameObject obj)
        {
            _dialogPanel.Close();
            _miningState.TargetSelected -= OnTargetSelected;
            _questsCreator.Created += OnQuestCreated;
        }

        private void OnQuestCreated()
        {
            _questsCreator.Created -= OnQuestCreated;
            Successor.Handle();
        }
    }
}
