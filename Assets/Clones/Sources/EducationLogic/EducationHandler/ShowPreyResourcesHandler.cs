using Clones.GameLogic;
using Clones.StateMachine;
using System;
using UnityEngine;

namespace Clones.EducationLogic
{
    public class ShowPreyResourcesHandler : EducationHandler
    {
        private readonly MiningState _miningState;
        private readonly IQuestsCreator _questsCreator;

        public ShowPreyResourcesHandler(MiningState miningState, IQuestsCreator questsCreator)
        {
            _miningState = miningState;
            _questsCreator = questsCreator;
        }

        public override void Handle()
        {
            Debug.Log("Сломай эти штуки. Для этого остановись возле них");

            _miningState.TargetSelected += OnTargetSelected;
        }

        private void OnTargetSelected(GameObject obj)
        {
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
