using Cinemachine;
using Clones.GameLogic;
using Clones.Input;
using Clones.StateMachine;
using Clones.UI;
using UnityEngine;

namespace Clones.EducationLogic
{
    public class ShowPreyResourcesHandler : EducationHandler
    {
        private const int VirtualCameraPriority = 17;
        private const int WaitingTime = 3;

        private readonly MiningState _miningState;
        private readonly IQuestsCreator _questsCreator;
        private readonly DialogPanel _dialogPanel;
        private readonly CinemachineVirtualCamera _virtualCamera;
        private readonly GameObject _controlObject;
        private readonly Waiter _waiter;

        public ShowPreyResourcesHandler(MiningState miningState, IQuestsCreator questsCreator, DialogPanel dialogPanel, CinemachineVirtualCamera virtualCamera, GameObject controlObject, Waiter waiter)
        {
            _miningState = miningState;
            _questsCreator = questsCreator;
            _dialogPanel = dialogPanel;
            _virtualCamera = virtualCamera;
            _controlObject = controlObject;
            _waiter = waiter;
        }

        public override void Handle()
        {
            _dialogPanel.Open();
            _virtualCamera.Priority = VirtualCameraPriority;
            _controlObject.GetComponent<IStopable>().Stop();
            _controlObject.SetActive(false);

            Debug.Log("Сломай эти штуки. Для этого остановись возле них");

            _waiter.Wait(WaitingTime, callback: OnCallback);

            _miningState.TargetSelected += OnTargetSelected;
        }

        private void OnCallback()
        {
            _virtualCamera.Priority = 0;
            _controlObject.SetActive(true);
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
