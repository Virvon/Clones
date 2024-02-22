using Clones.UI;
using UnityEngine;

namespace Clones.EducationLogic
{
    public class ShowSecondQuestHandler : EducationHandler
    {
        private const float WaitingTime = 3;

        private readonly DialogPanel _dialogPanel;
        private readonly Waiter _waiter;

        public ShowSecondQuestHandler(DialogPanel dialogPanel, Waiter waiter)
        {
            _dialogPanel = dialogPanel;
            _waiter = waiter;
        }

        public override void Handle()
        {
            _dialogPanel.Open();
            Debug.Log("Выполни следующее задание и покончим с этим");

            _waiter.Wait(WaitingTime, callback: OnCallback);
        }

        private void OnCallback()
        {
            _dialogPanel.Close();
            Successor.Handle();
        }
    }
}
